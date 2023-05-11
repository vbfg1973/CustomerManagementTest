using CustomerManagement.Domain.Customers.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{
    /// <summary>
    ///     Handles operations of customers
    /// </summary>
    public class CustomerController : BaseV1ApiController
    {
        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CustomerController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        ///     Returns a customer identified by the customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryCustomersByPages"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomerByPagedQuery([FromQuery] QueryCustomersByPages queryCustomersByPages, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(queryCustomersByPages, cancellationToken);
            return Ok(result);
        }
        
        /// <summary>
        ///     Returns a customer identified by the customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
        {
            var request = new QueryCustomerById { Id = id };
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}