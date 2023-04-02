using System;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Requests.Models
{
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Partial class that represents the Aggregate root Dimming Calendar
    /// </summary>
    public partial class WebChangeRequest
    {
        /// <summary>
        /// Dimming Program associated to each day of week
        /// </summary>
        public class DimmingProgramWeekDayWebRequest : IDimmingProgramWeekDay
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