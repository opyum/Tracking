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
        /// Dimming Program associated to exception day in an event
        /// </summary>
        public class DimmingProgramExceptionDayDto : IDimmingProgramExceptionDay
        {
            /// <summary>
            /// Gets the start date of the exception
            /// </summary>
            public DateOnly StartDate { get; init; }

            /// <summary>
            /// Gets the end date of the exception
            /// </summary>
            public DateOnly EndDate { get; init; }

            /// <summary>
            /// Gets the Dimming Program identifier
            /// </summary>
            public Guid DimmingProgramId { get; init; }

            /// <summary>
            /// Gets the exception type (fixed or calendar)
            /// </summary>
            public ExceptionType? ExceptionType { get; init; }
        }
    }
}