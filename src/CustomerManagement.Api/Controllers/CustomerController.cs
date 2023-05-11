using AutoMapper;
using CustomerManagement.Api.Extensions;
using CustomerManagement.Domain.Customers.Features.Commands.CustomerCreate;
using CustomerManagement.Domain.Customers.Features.Queries.CustomerById;
using CustomerManagement.Domain.Customers.Features.Queries.CustomerByPages;
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

        #region Customer commands

        /// <summary>
        /// </summary>
        /// <param name="customerCreateCommandDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateCommandDto customerCreateCommandDto,
            CancellationToken cancellationToken)
        {
            var customerCreateCommand =
                _mapper.Map<CustomerCreateCommandDto, CustomerCreateCommand>(customerCreateCommandDto);
            customerCreateCommand.CorrelationId = Request.GetCorrelationId();

            var customerWithAllDetailsResponseDto = await Mediator.Send(customerCreateCommand, cancellationToken);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customerWithAllDetailsResponseDto.Id },
                customerWithAllDetailsResponseDto);
        }

        #endregion

        #region Customer queries

        /// <summary>
        ///     Returns a customer identified by the customer ID
        /// </summary>
        /// <param name="customersByPagesQueryDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomerByPagedQuery(
            [FromQuery] CustomersByPagesQueryDto customersByPagesQueryDto, CancellationToken cancellationToken)
        {
            var queryCustomersByPages =
                _mapper.Map<CustomersByPagesQueryDto, CustomerByPagesQuery>(customersByPagesQueryDto);
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
            var queryCustomerById = new CustomerByIdQuery { Id = id, CorrelationId = Request.GetCorrelationId() };
            var result = await Mediator.Send(queryCustomerById, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}