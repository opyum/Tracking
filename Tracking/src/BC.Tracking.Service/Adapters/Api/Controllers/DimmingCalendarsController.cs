using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Service.Adapters.Api.Controllers
{
    using Connexity.BC.Tracking.Domain.Events.Models;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;
    using DomainRequests = Domain.Requests;
    using WebRequests = Requests;

    /// <summary>
    /// Main controller for Dimming Calendar aggregate root
    /// </summary>
    [Route(Constants.Routes.TrackingsV1)]
    [ApiController]
    public class TrackingsController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TrackingsController> _logger;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingsController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="mediator">The mediator.</param>
        public TrackingsController(ILogger<TrackingsController> logger, IMapper mapper, IMediator mediator)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._mediator = mediator;
        }

        /// <summary>
        /// Get one dimming calendar asynchronous.
        /// </summary>
        /// <returns>The dimming calendar with the asked id</returns>
        [HttpGet("{id}", Name = "GetTrackingAsync")]
        public async Task<IActionResult> GetTrackingAsync(Guid id)
        {
            // No try catch because the errors handling are centralised in the ErrorController
            var result = await this._mediator.Send(new DomainRequests.DetailRequest { TrackingId = id });
            var model = this._mapper.Map<Models.Tracking>(result);
            return this.Ok(model);
        }

        /// <summary>
        /// Creates the dimming calendar asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The newly created dimming calendar</returns>
        [HttpPost]
        public async Task<IActionResult> InitCreateTrackingAsync(WebRequests.CreateTrackingWebRequest request)
        {
            this._logger.LogDebug("Dimming calendar creation initiated with payload: {@Payload}", request);

            await this._mediator.Send(new DomainRequests.CreateRequest()
            {
                Label = request.Label,
                Description = request.Description,
                Code = request.Code,
                Mode = request.Mode,
                ValidationErrors = new List<ValidationError>()
            });

            return this.Accepted();
        }
    }
}