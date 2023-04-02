using MediatR;
using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.Requests
{
    using Connexity.BC.Tracking.Domain.Events.Models;
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;

    /// <summary>
    /// The create request to effectively create a new dimming calendar
    /// </summary>
    public partial class CreateRequest : Request,
        IRequest<Entities.Tracking>,
        IChangeRequest,
        ITracking<DimmingProgramWeekDayRequest, DimmingProgramExceptionDayRequest>
    {
        /// <summary>
        /// Gets identifier.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; init; }

        /// <summary>
        /// Gets the Code.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets the operating mode.
        /// </summary>
        public OperatingMode? Mode { get; init; }

        /// <summary>
        /// Gets the correlation Id
        /// </summary>
        public Guid CorrelationId { get; init; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public List<ValidationError> ValidationErrors { get; init; }

        /// <summary>
        /// Identifier of the user that has made the request
        /// </summary>
        public string UserId { get; init; }
    }
}