using System;

namespace Connexity.BC.Tracking.Domain.Requests.Models
{
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Partial class that represents a base request
    /// </summary>
    public partial class Request
    {
        /// <summary>
        /// Dimming Program associated to each day of week
        /// </summary>
        public class DimmingProgramWeekDayRequest : IDimmingProgramWeekDay
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