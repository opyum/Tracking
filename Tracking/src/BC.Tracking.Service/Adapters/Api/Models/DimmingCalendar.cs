using System;
using System.Collections.Generic;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Models
{
    using Connexity.BC.Tracking.Domain.ValueObject;

    public partial class Tracking
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
        public OperatingMode? Mode { get; set; }

        /// <summary>
        /// Gets the dimming program identifier for each day of week.
        /// </summary>
        public List<DimmingProgramWeekDay> DimmingProgramsWeekDays { get; set; }

        /// <summary>
        /// Gets the dimming program identifier for the exceptions day.
        /// </summary>
        public List<DimmingProgramExceptionDay> DimmingProgramsExceptionDays { get; set; }
    }
}