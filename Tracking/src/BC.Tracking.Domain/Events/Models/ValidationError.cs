using System;

namespace Connexity.BC.Tracking.Domain.Events.Models
{
    /// <summary>
    /// Represents a validation error encountered during a choregraphy event
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Identifier of µService
        /// </summary>
        public string AppId { get; init; }

        /// <summary>
        /// Timestamp of error
        /// </summary>
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}