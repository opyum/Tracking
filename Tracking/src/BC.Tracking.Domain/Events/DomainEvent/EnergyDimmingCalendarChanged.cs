using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.Events
{
    using Connexity.BC.Tracking.Domain.Events.Models;
    using Connexity.BC.Tracking.Domain.Helpers;
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;

    /// <summary>
    /// The event sent when a dimming calendar has been effectively changed (CREATED, UPDATED, DELETED)
    /// </summary>
    public class EnergyTrackingChanged<TDimmingProgramWeekDay, TDimmingProgramExceptionDay> : DomainEvent
        where TDimmingProgramWeekDay : IDimmingProgramWeekDay
        where TDimmingProgramExceptionDay : IDimmingProgramExceptionDay
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyTrackingChanged"/> class.
        /// </summary>
        public EnergyTrackingChanged(
            ITracking<TDimmingProgramWeekDay, TDimmingProgramExceptionDay> Tracking,
            EventType eventType,
            Guid correlationId,
            List<ValidationError> validationErrors = null
            ) : base(eventType, correlationId, validationErrors)
        {
            this.Content = new TrackingDto()
            {
                Id = Tracking.Id,
                Label = Tracking.Label,
                Description = Tracking.Description,
                Code = Tracking.Code,
                Mode = Tracking.Mode,
            };
        }
    }
}