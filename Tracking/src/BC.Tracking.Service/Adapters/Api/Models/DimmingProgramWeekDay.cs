using System;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Models
{
    using Connexity.BC.Tracking.Domain.Interfaces;

    /// <summary>
    /// Partial class that represents the Aggregate root Dimming Calendar
    /// </summary>
    public partial class Tracking
    {
        /// <summary>
        /// Dimming Program associated to each day of week
        /// </summary>
        public class DimmingProgramWeekDay : IDimmingProgramWeekDay
        {
            /// <summary>
            /// Gets the day of week
            /// </summary>
            public Domain.ValueObject.DayOfWeek? DayOfWeek { get; init; }

            /// <summary>
            /// Gets the identifier of the dimming program
            /// </summary>
            public Guid DimmingProgramId { get; init; }
        }
    }
}