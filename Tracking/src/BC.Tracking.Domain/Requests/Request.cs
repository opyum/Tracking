using System;

namespace Connexity.BC.Tracking.Domain.Requests
{
    /// <summary>
    /// Class that represents the base of a Mediator request
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// Gets the request identifier.
        /// </summary>
        public Guid RequestId { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
    }
}