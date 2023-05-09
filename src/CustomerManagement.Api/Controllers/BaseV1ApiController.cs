using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{
    /// <summary>
    ///     Abstract base api controller for all other controllers to inhereit from
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseV1ApiController : ControllerBase
    {
        /// <summary>
        ///     The mediator object for decoupling messages from their handlers
        /// </summary>
        protected readonly IMediator Mediator;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="mediator"></param>
        protected BaseV1ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        ///     Get the correlationId from the request
        /// </summary>
        /// <returns></returns>
        protected Guid GetCorrelationId()
        {
            var correlationId = HttpContext?.Request?.Headers["X-Correlation-ID"].FirstOrDefault();
            if (string.IsNullOrEmpty(correlationId)) correlationId = Guid.NewGuid().ToString();

            // TODO - this ought to follow TryParse pattern and return a meaningful exception on failure
            return Guid.Parse(correlationId);
        }
    }
}