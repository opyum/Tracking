using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Domain.Entities
{ 
    using Connexity.BC.Tracking.Domain.Events;
    using Connexity.BC.Tracking.Domain.Events.Models;
    using Connexity.BC.Tracking.Domain.Exceptions;
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.Ports;
    using Connexity.BC.Tracking.Domain.Requests;
    using Connexity.BC.Tracking.Domain.States;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;

    /// <summary>
    /// Represent the Aggregate root of the Domain
    /// It has all the method to handle the get, create and validate the Dimming Calendars
    /// </summary>
    public partial class Tracking : IAggregateRoot,
                           IRequestHandler<CreateRequest, Tracking>,
                           IRequestHandler<DetailRequest, Tracking>,
                           IRequestHandler<InitCreateRequest, Unit>

    {
        /// <summary>
        /// The store port
        /// </summary>
        private readonly IStorePort _storePort;

        /// <summary>
        /// The notify port
        /// </summary>
        private readonly INotifyPort _notifyPort;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The validator factory
        /// </summary>
        private readonly IValidatorFactory _validatorFactory;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<Tracking> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tracking" /> class representing the Aggregate root.
        /// </summary>
        public Tracking(
            IStorePort storePort,
            INotifyPort notifyPort,
            IMapper mapper,
            IValidatorFactory validatorFactory,
            ILogger<Tracking> logger)
        {
            this._storePort = storePort;
            this._notifyPort = notifyPort;
            this._mapper = mapper;
            this._validatorFactory = validatorFactory;
            this._logger = logger;
        }

        /// <summary>
        /// Gets the Tracking identifier.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// Gets the Code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the operating mode.
        /// </summary>
        public OperatingMode? Mode { get; private set; }

        /// <summary>
        /// Get one asynchronous
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Tracking> GetOneAsync(Guid TrackingId, CancellationToken cancellationToken = default)
        {
            this._logger.LogDebug("GetOneRequest handled with id: {TrackingId}", TrackingId);

            var state = await this._storePort.GetAsync(TrackingId, cancellationToken);

            if (state == null)
                throw new NoTrackingsFoundException($"Dimming Calendar not found with Id {TrackingId}");

            this.LoadState(state);

            return this;
        }

        /// <summary>
        /// Initiate a create dimming calendar choregraphy.
        /// </summary>
        /// <param name="request">The payload</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task InitCreateAsync(InitCreateRequest request, CancellationToken cancellationToken = default)
        {
            this._logger.LogDebug("InitCreateRequest handled with payload: {@payload}", request);

            request = await this.ValidateRequest<InitCreateRequest>(request, cancellationToken);

            this._logger.LogInformation("InitCreateRequest validation with {NbrOfErrors} error(s): {@Errors}", request.ValidationErrors.Count, request.ValidationErrors);

            this.Id = Guid.NewGuid();
            this.Label = request.Label;
            this.Description = request.Description;
            this.Code = request.Code;
            this.Mode = request.Mode;

            //await this._notifyPort.NotifyAsync(new EnergyTrackingChangeRequested(
            //    this,
            //    request.UserId,
            //    request.ValidationErrors,
            //    EventType.CREATING,
            //    Guid.NewGuid()
            //), cancellationToken);

            this._logger.LogDebug("DimmingPrograms-TrackingChangeRequested CREATING published");
        }

        /// <summary>
        /// Creates a dimming calendar asynchronous.
        /// </summary>
        /// <param name="request">The payload</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Tracking> CreateAsync(CreateRequest request, CancellationToken cancellationToken = default)
        {
            this._logger.LogDebug("CreateRequest handled with payload: {@payload}", request);
            // 1. validation request
            request = await this.ValidateRequest<CreateRequest>(request, cancellationToken);

            this._logger.LogInformation("CreateRequest validation with {NbrOfErrors} error(s): {@Errors}", request.ValidationErrors.Count, request.ValidationErrors);

            if (request.ValidationErrors.Count > 0)
            {
                this._logger.LogError("Dimming Calendar creation failed: {@Errors}", request.ValidationErrors);

                await this._notifyPort.NotifyAsync(new EnergyTrackingChanged<DimmingProgramWeekDayRequest, DimmingProgramExceptionDayRequest>(
                    request,
                    EventType.NOTCREATED,
                    request.CorrelationId,
                    request.ValidationErrors
                ), cancellationToken);

                this._logger.LogDebug("Energy-TrackingChanged NOTCREATED published");
                return this;
            }

            this.Id = Guid.NewGuid();
            this.Label = request.Label;
            this.Description = request.Description;
            this.Code = request.Code;
            this.Mode = request.Mode;

            await this.SaveAsync(cancellationToken);

            this._logger.LogInformation("Dimming Calendar creation succeed: {@DimCal}", this);
            //var energyTrackingChanged = new EnergyTrackingChanged<DimmingProgramWeekDayEntity, DimmingProgramExceptionDayEntity>(
            //    this,
            //    EventType.CREATED,
            //    request.CorrelationId);

            // 3. send notifying to the bus
            //_ = Task.Run(() => this._notifyPort.NotifyAsync(energyTrackingChanged, cancellationToken), cancellationToken);
            this._logger.LogDebug("Energy-TrackingChanged CREATED published");
            // 4. send historization to the bus
            return this;
        }

        /// <summary>
        /// Loads asynchronous.
        /// </summary>
        /// <param name="Id">The Tracking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NoTrackingsFoundException">Tracking not found.</exception>
        private async Task LoadAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var state = await this._storePort.GetAsync(Id, cancellationToken);

            if (state == null)
                throw new NoTrackingsFoundException($"Dimming Calendar not found with Id {Id}");

            this.LoadState(state);
        }

        /// <summary>
        /// Loads the state asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private void LoadState(TrackingState state)
        {
            this._mapper.Map(state, this);
        }

        /// <summary>
        /// Changes asynchronous.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task ChangeAsync(UpdateRequest request, CancellationToken cancellationToken = default)
        {
            request = await this.ValidateRequest<UpdateRequest>(request, cancellationToken);

            await this.LoadAsync(request.Id, cancellationToken);

            this.Id = request.Id;
            this.Label = request.Label;
            this.Description = request.Description;
            this.Code = request.Code;
            this.Mode = request.Mode;

            await this.SaveAsync(cancellationToken);
            //await this._notifyPort.NotifyAsync(new EnergyTrackingChanged<DimmingProgramWeekDayEntity, DimmingProgramExceptionDayEntity>(
            //    this,
            //    EventType.UPDATED,
            //    Guid.NewGuid()
            //    ), cancellationToken);
        }

        /// <summary>
        /// Saves asynchronous.
        /// </summary>
        private async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            TrackingState state = this._mapper.Map<TrackingState>(this);
            await this._storePort.SaveAsync(state, cancellationToken);
        }

        /// <summary>
        /// Handles a create request
        /// </summary>
        public async Task<Unit> Handle(InitCreateRequest request, CancellationToken cancellationToken)
        {
            this._logger.LogDebug("InitCreateRequest: Accepted");

            await this.InitCreateAsync(request, cancellationToken);
            return Unit.Value;
        }

        /// <summary>
        /// Handles a create request
        /// </summary>
        /// <returns>
        /// The created dimming calendar
        /// </returns>
        public async Task<Tracking> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            this._logger.LogDebug("CreateRequest: Accepted");

            await this.CreateAsync(request, cancellationToken);
            return this;
        }

        /// <summary>
        /// Handles an update request.
        /// </summary>
        public async Task<Unit> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            await this.ChangeAsync(request, cancellationToken);
            return Unit.Value;
        }

        /// <summary>
        /// Handles a get one request
        /// </summary>
        /// <returns>
        /// A dimming calendar
        /// </returns>
        public async Task<Tracking> Handle(DetailRequest request, CancellationToken cancellationToken)
        {
            return await this.GetOneAsync(request.TrackingId, cancellationToken);
        }

        /// <summary>
        /// Validate the data of a request
        /// </summary>
        private async Task<T> ValidateRequest<T>(IChangeRequest request, CancellationToken cancellationToken) where T : IChangeRequest
        {
            var validator = this._validatorFactory.GetValidator<T>();
            var result = await validator.ValidateAsync((T)request, cancellationToken);

            if (result.Errors.Count > 0)
            {
                foreach (ValidationFailure error in result.Errors)
                {
                    request.ValidationErrors.Add(
                        new ValidationError
                        {
                            AppId = Constants.AppId,
                            Timestamp = DateTimeOffset.UtcNow,
                            Code = (int)HttpStatusCode.BadRequest,
                            Message = error.ErrorMessage,
                        });
                }
            }

            var foundTrackingState = await this._storePort.GetByCodeAsync(request.Code, cancellationToken);

            if (foundTrackingState != null)
            {
                this._logger.LogError("Dimming Calendar creation failed: Code must be unique");
                throw new ValidationException("Code must be unique");
            }

            return (T)request;
        }
    }
}