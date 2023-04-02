using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.Events
{
    using Connexity.BC.Tracking.Domain.Events.Models;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Represents the event emitted by the Energy domain
    /// </summary>
    public abstract class DomainEvent
    {
        protected DomainEvent(EventType eventType, Guid correlationId, List<ValidationError> validationErrors)
        {
            this.EventType = eventType;
            this.CorrelationId = correlationId;
            this.ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        public Guid EventId { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets the timestamp of the emitted event.
        /// </summary>
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Gets the event type.
        /// </summary>
        public EventType EventType { get; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        public Guid CorrelationId { get; }

        /// <summary>
        /// The payload of the event
        /// </summary>
        public TrackingDto Content { get; set; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public List<ValidationError> ValidationErrors { get; }
    }
}