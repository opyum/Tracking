using System;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Requests
{
    /// <summary>
    /// The update Tracking web request.
    /// </summary>
    public class UpdateTrackingWebRequest : WebChangeRequest
    {
        /// <summary>
        /// Gets the Tracking identifier.
        /// </summary>
        public Guid Id { get; init; }
    }
}