using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Controllers
{
    using Connexity.BC.Tracking.Domain.Events;
    using Connexity.BC.Tracking.Domain.Requests;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;

    /// <summary>
    /// Controller to handle the events received regarding the dimming calendars
    /// </summary>
    [Route(Constants.Routes.TrackingsEventHandler)]
    [ApiController]
    public class TrackingsEventHandlerController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TrackingsEventHandlerController> _logger;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingsEventHandlerController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mediator">The mediator.</param>
        public TrackingsEventHandlerController(ILogger<TrackingsEventHandlerController> logger, IMediator mediator, IMapper mapper)
        {
            this._logger = logger;
            this._mediator = mediator;
            this._mapper = mapper;
        }

        /// <summary>
        /// Receive a Energy-TrackingChangeRequested event and log it.
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        [HttpPost(Constants.Routes.TrackingChangeRequestedCreating)]
        public async Task<IActionResult> OnTrackingChangeRequestedCreating([FromBody] EnergyTrackingChangeRequested evt)
        {
            this._logger.LogDebug("Trackings-TrackingChangeRequested event received : {@Event}", evt);

            await this._mediator.Send(new CreateRequest()
            {
                UserId = evt.UserId,
                Id = evt.Content.Id,
                Label = evt.Content.Label,
                Description = evt.Content.Description,
                Code = evt.Content.Code,
                Mode = evt.Content.Mode,
                CorrelationId = evt.CorrelationId,
                ValidationErrors = evt.ValidationErrors
            });

            return this.NoContent();
        }
    }
}