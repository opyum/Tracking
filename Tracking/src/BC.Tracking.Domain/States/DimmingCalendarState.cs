using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.States
{
    using Connexity.BC.Tracking.Domain.ValueObject;

    /// <summary>
    /// The state from Redis of a Dimming Calendar
    /// </summary>
    public partial class TrackingState
    {
        /// <summary>
        /// Gets the Tracking identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets the Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the operating mode.
        /// </summary>
        public OperatingMode Mode { get; set; }
    }
}