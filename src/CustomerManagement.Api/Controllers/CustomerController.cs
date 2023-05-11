using AutoMapper;
using CustomerManagement.Api.Extensions;
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
        private readonly IMapper _mapper;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public CustomerController(IMediator mediator, IMapper mapper) : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Returns a customer identified by the customer ID
        /// </summary>
        /// <param name="queryDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomerByPagedQuery([FromQuery] QueryCustomersByPagesDto queryDto, CancellationToken cancellationToken)
        {
            var queryCustomersByPages = _mapper.Map<QueryCustomersByPagesDto, QueryCustomersByPages>(queryDto);
            queryCustomersByPages.CorrelationId = Request.GetCorrelationId();
            
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
            var queryCustomerById = new QueryCustomerById { Id = id, CorrelationId = Request.GetCorrelationId()};
            var result = await Mediator.Send(queryCustomerById, cancellationToken);
            return Ok(result);
        }
    }
}