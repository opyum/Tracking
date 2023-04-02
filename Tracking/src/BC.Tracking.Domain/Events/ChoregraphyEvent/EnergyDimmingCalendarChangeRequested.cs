using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Connexity.BC.Tracking.Domain.Events
{
    using Connexity.BC.Tracking.Domain.Events.Models;
    using Connexity.BC.Tracking.Domain.Helpers;
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;

    /// <summary>
    /// The initial event request to start a dimming calendar changed choregraphy
    /// </summary>
    public class EnergyTrackingChangeRequested : ChoregraphyEvent
    {
        [JsonConstructor]
        public EnergyTrackingChangeRequested(
            string userId,
            List<ValidationError> validationErrors,
            EventType eventType,
            Guid correlationId
            ) : base(userId, eventType, correlationId, validationErrors)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyTrackingChangeRequested"/> class.
        /// </summary>
        public EnergyTrackingChangeRequested(
            ITracking<DimmingProgramWeekDayEntity, DimmingProgramExceptionDayEntity> Tracking,
            string userId,
            List<ValidationError> validationErrors,
            EventType eventType,
            Guid correlationId
            ) : base(userId, eventType, correlationId, validationErrors)
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