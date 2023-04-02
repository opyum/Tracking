using System;

namespace Connexity.BC.Tracking.Domain.Interfaces
{
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Interface that represents the base of a Dimming Program Week Day
    /// </summary>
    public interface IDimmingProgramWeekDay
    {
        /// <summary>
        /// Gets the day of week
        /// </summary>
        DayOfWeek? DayOfWeek { get; init; }

        /// <summary>
        /// Gets the Dimming Program identifier
        /// </summary>
        Guid DimmingProgramId { get; init; }
    }
}