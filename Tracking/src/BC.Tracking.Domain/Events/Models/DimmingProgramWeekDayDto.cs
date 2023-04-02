using System;

namespace Connexity.BC.Tracking.Domain.Events.Models
{
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Partial class that represents he dimming calendar model for the events
    /// </summary>
    public partial class TrackingDto
    {
        /// <summary>
        /// Dimming Program associated to each day of week in an event
        /// </summary>
        public class DimmingProgramWeekDayDto : IDimmingProgramWeekDay
        {
            /// <summary>
            /// Gets the day of week
            /// </summary>
            public DayOfWeek? DayOfWeek { get; init; }

            /// <summary>
            /// Gets the identifier of the dimming program
            /// </summary>
            public Guid DimmingProgramId { get; init; }
        }
    }
}