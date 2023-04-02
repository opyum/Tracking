using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.Interfaces
{
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// Interface that represents the base of a Dimming Calendar
    /// </summary>
    public interface ITracking<TDimmingProgramWeekDay, TDimmingProgramExceptionDay>
    {
        /// <summary>
        /// Gets the Tracking identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Gets the Code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the operating mode.
        /// </summary>
        public OperatingMode? Mode { get; }
    }
}