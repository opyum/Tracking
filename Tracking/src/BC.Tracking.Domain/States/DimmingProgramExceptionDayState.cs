using System;

namespace Connexity.BC.Tracking.Domain.States
{
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Partial class that represents the Aggregate root Dimming Calendar
    /// </summary>
    public partial class TrackingState
    {
        /// <summary>
        /// Dimming Program associated to each day of week
        /// </summary>
        public class DimmingProgramExceptionDayState : IDimmingProgramExceptionDay
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