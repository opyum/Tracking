using System;

namespace Connexity.BC.Tracking.Domain.Entities
{
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Partial class that represents the Aggregate root Dimming Calendar
    /// </summary>
    public partial class Tracking
    {
        /// <summary>
        /// Dimming Program associated to each day of week
        /// </summary>
        public class DimmingProgramWeekDayEntity : IDimmingProgramWeekDay
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