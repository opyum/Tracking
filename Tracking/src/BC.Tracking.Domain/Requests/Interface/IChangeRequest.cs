using System.Collections.Generic;

namespace Connexity.BC.Tracking.Domain.Requests
{
    using Connexity.BC.Tracking.Domain.Events.Models;

    /// <summary>
    /// Represents a mediator request to that will change the aggregate root
    /// </summary>
    internal interface IChangeRequest
    {
        /// <summary>
        /// Identifier of the user that has made the request
        /// </summary>
        public string UserId { get; init; }

        /// <summary>
        /// Gets the Code.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public List<ValidationError> ValidationErrors { get; init; }
    }
}