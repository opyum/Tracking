using System;

namespace Connexity.BC.Tracking.Domain.Interfaces
{
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Interface that represents the base of a Dimming Program Exception Day
    /// </summary>
    public interface IDimmingProgramExceptionDay
    {
        /// <summary>
        /// Gets the start date of the exception
        /// </summary>
        DateOnly StartDate { get; init; }

        /// <summary>
        /// Gets the end date of the exception
        /// </summary>
        DateOnly EndDate { get; init; }

        /// <summary>
        /// Gets the Dimming Program identifier
        /// </summary>
        Guid DimmingProgramId { get; init; }

        /// <summary>
        /// Gets the exception type (fixed or calendar)
        /// </summary>
        ExceptionType? ExceptionType { get; init; }
    }
}