using AutoMapper;
using CustomerManagement.Api.Extensions;
using CustomerManagement.Domain.Customers.Features.Commands.AddAddressToCustomer;
using CustomerManagement.Domain.Customers.Features.Commands.CustomerCreate;
using CustomerManagement.Domain.Customers.Features.Commands.CustomerDelete;
using CustomerManagement.Domain.Customers.Features.Commands.SetAddressToDefault;
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

        #region Address commands

        /// <summary>
        ///     Add address to an already existing customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addAddressToCustomerDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{id:guid}/address")]
        public async Task<IActionResult> AddAddressToCustomer(Guid id,
            [FromBody] AddAddressToCustomerDto addAddressToCustomerDto,
            CancellationToken cancellationToken)
        {
            var addAddressToCustomerCommand =
                _mapper.Map<AddAddressToCustomerDto, AddAddressToCustomerCommand>(addAddressToCustomerDto);
            addAddressToCustomerCommand.CorrelationId = Request.GetCorrelationId();
            addAddressToCustomerCommand.CustomerId = id;

            var customerWithAllDetailsResponseDto = await Mediator.Send(addAddressToCustomerCommand, cancellationToken);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customerWithAllDetailsResponseDto.Id },
                customerWithAllDetailsResponseDto);
        }

        /// <summary>
        ///     Set an existing address to be the default for the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="addressId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{customerId:guid}/address/{addressId:guid}/default")]
        public async Task<IActionResult> SetDefaultCustomerAddress(Guid customerId, Guid addressId,
            CancellationToken cancellationToken)
        {
            var setAddressToDefaultCommand = new SetAddressToDefaultCommand
            {
                CustomerId = customerId,
                AddressId = addressId,
                CorrelationId = Request.GetCorrelationId()
            };

            var customerWithAllDetailsResponseDto = await Mediator.Send(setAddressToDefaultCommand, cancellationToken);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customerWithAllDetailsResponseDto.Id },
                customerWithAllDetailsResponseDto);
        }

        #endregion

        #region Customer commands

        /// <summary>
        ///     Creates a customer
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

        /// <summary>
        ///     Deletes the customer identified by the customer ID, plus associated contact details and addresses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id, CancellationToken cancellationToken)
        {
            var customerDeleteCommand = new CustomerDeleteCommand
                { Id = id, CorrelationId = Request.GetCorrelationId() };

            await Mediator.Send(customerDeleteCommand, cancellationToken);

            return Ok();
        }

        #endregion

        #region Customer queries

        /// <summary>
        ///     Returns customers meeting search criteria
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