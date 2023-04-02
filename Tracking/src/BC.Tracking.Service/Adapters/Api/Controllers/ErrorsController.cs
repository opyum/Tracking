using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Controllers
{
    using Connexity.BC.Tracking.Domain.Exceptions;

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(Constants.Routes.Errors)]
    public class ErrorsController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ErrorsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ErrorsController(ILogger<ErrorsController> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Handles the errors.
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public IActionResult HandleErrors()
        {
            var contextException = this.HttpContext.Features.Get<IExceptionHandlerFeature>(); // Capture the exception.
            this._logger.LogError("Error found {@Error} with message {@Message}", contextException.Error, contextException.Error.Message);

            HttpStatusCode responseStatusCode;
            var exception = contextException.Error;

            if (exception is NoTrackingsFoundException)
            {
                responseStatusCode = HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException)
            {
                responseStatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                responseStatusCode = HttpStatusCode.ServiceUnavailable;
            }

            return this.Problem(contextException.Error.Message, statusCode: (int)responseStatusCode);
        }
    }
}