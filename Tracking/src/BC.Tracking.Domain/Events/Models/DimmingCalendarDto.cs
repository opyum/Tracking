using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Connexity.BC.Tracking.Domain.Events.Models
{
    
    using Connexity.BC.Tracking.Domain.Interfaces;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;

    /// <summary>
    /// The dimming calendar model for the events
    /// </summary>
    public partial class TrackingDto : ITracking<DimmingProgramWeekDayDto, DimmingProgramExceptionDayDto>
    {
        /// <summary>
        /// Gets the Tracking identifier.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; init; }

        /// <summary>
        /// Gets the Code.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets the operating mode.
        /// </summary>
        public OperatingMode? Mode { get; init; }


#nullable enable

        /// <summary>
        /// Gets or sets the unknown fields.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? UnknownFields { get; init; }

#nullable disable
    }
}