using AutoMapper;
using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Customers.Features.Queries.CustomerById;
using CustomerManagement.Domain.Customers.Responses;
using CustomerManagement.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Features.Commands.AddAddressToCustomer
{
    /// <summary>
    ///     Handler for adding address to customer
    /// </summary>
    public class AddAddressToCustomerCommandHandler :
        IRequestHandler<AddAddressToCustomerCommand, CustomerWithAllDetailsResponseDto>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<AddAddressToCustomerCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        /// <summary>
        ///     ctor
        /// </summary>
        public AddAddressToCustomerCommandHandler(CustomerManagementContext context, IMediator mediator, IMapper mapper,
            ILogger<AddAddressToCustomerCommandHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     Handle command for adding an address to a specified customer identified by their ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CustomerWithAllDetailsResponseDto> Handle(AddAddressToCustomerCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message($"Attempting to add an address to a customer identified by {request.CustomerId}"),
                LogFmt.CorrelationId(request));

            // Get a 404 if customer doesn't exist
            if (!_context.Customers.Any(customer => customer.Id == request.CustomerId))
                throw new ResourceNotFoundException(ExceptionMessages.CustomerDoesNotExist);

            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);


            // Build the address
            var address = _mapper.Map<AddAddressToCustomerCommand, Address>(request);
            address.Id = Guid.NewGuid();

            // Get the customer
            var customer = _context.Customers.First(cus => cus.Id == request.CustomerId);

            // Build the join table
            var customerAddressJoin = new CustomerAddress
            {
                AddressId = address.Id,
                CustomerId = request.CustomerId,
                IsDefaultAddress = false
            };

            // Save it out
            _context.Addresses.Add(address);
            _context.CustomerAddresses.Add(customerAddressJoin);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message($"Added an address to a customer identified by {request.CustomerId}"),
                LogFmt.CorrelationId(request));

            // Get the whole customer and return
            var customerFull =
                await _mediator.Send(
                    new CustomerByIdQuery { Id = request.CustomerId, CorrelationId = request.CorrelationId },
                    cancellationToken);

            return customerFull;
        }
    }
}