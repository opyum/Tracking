using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.Events
{
    using Connexity.BC.Tracking.Domain.Events.Models;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Represents an event involved in a choregraphy
    /// </summary>
    public abstract class ChoregraphyEvent : DomainEvent
    {
        protected ChoregraphyEvent(
            string userId,
            EventType eventType,
            Guid correlationId,
            List<ValidationError> validationErrors
            ): base(eventType, correlationId, validationErrors)
        {
            this.UserId = userId;
        }

        /// <summary>
        /// Identifier of the user that has made the request
        /// </summary>
        public string UserId { get; init; }
    }
}