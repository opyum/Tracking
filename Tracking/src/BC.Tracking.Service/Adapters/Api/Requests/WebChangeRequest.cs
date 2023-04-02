using System.Collections.Generic;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Requests
{
    using Connexity.BC.Tracking.Domain.ValueObject;
    using static Connexity.BC.Tracking.Service.Adapters.Api.Requests.Models.WebChangeRequest;

    /// <summary>
    /// Represent a change request sent from the client
    /// </summary>
    public partial class WebChangeRequest
    {
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

    }
}