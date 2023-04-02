using MediatR;
using System;

namespace Connexity.BC.Tracking.Domain.Requests
{
    using Connexity.BC.Tracking.Domain.Entities;

    /// <summary>
    /// The detail request to get a dimming calendar by its Id
    /// </summary>
    public class DetailRequest : Request, IRequest<Tracking>
    {
        /// <summary>
        /// Gets the dimming calendar identifier.
        /// </summary>
        public Guid TrackingId { get; init; }
    }
}