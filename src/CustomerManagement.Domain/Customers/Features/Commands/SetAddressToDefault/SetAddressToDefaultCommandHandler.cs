using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Domain.Customers.Features.Queries.CustomerById;
using CustomerManagement.Domain.Customers.Responses;
using CustomerManagement.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Features.Commands.SetAddressToDefault
{
    /// <summary>
    ///     Handler for set address to default command
    /// </summary>
    public class SetAddressToDefaultCommandHandler :
        IRequestHandler<SetAddressToDefaultCommand, CustomerWithAllDetailsResponseDto>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<SetAddressToDefaultCommandHandler> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public SetAddressToDefaultCommandHandler(CustomerManagementContext context,
            IMediator mediator,
            ILogger<SetAddressToDefaultCommandHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        ///     Handler of commands
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CustomerWithAllDetailsResponseDto> Handle(SetAddressToDefaultCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message(
                    $"Attempting to set address ({request.AddressId}) as default for customer {request.CustomerId}"),
                LogFmt.CorrelationId(request.CorrelationId));

            await _context.Database.BeginTransactionAsync(cancellationToken);

            if (!_context.CustomerAddresses.Any(customerAddress =>
                    customerAddress.AddressId == request.AddressId && customerAddress.CustomerId == request.CustomerId))
                throw new ResourceNotFoundException("Address does not exist in association with this customer");

            ClearExistingDefaultAddresses(request);
            SetDefaultAddress(request);

            await _context.SaveChangesAsync(cancellationToken);

            await _context.Database.CommitTransactionAsync(cancellationToken);

            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message(
                    $"Address ({request.AddressId}) has been set as the default for customer {request.CustomerId}"),
                LogFmt.CorrelationId(request.CorrelationId));

            // Get the whole customer and return
            var customerFull =
                await _mediator.Send(
                    new CustomerByIdQuery { Id = request.CustomerId, CorrelationId = request.CorrelationId },
                    cancellationToken);

            return customerFull;
        }

        /// <summary>
        ///     Get the specific address and set to default
        /// </summary>
        /// <param name="request"></param>
        private void SetDefaultAddress(SetAddressToDefaultCommand request)
        {
            var newDefaultAddress = _context.CustomerAddresses.First(customerAddress =>
                customerAddress.CustomerId == request.CustomerId &&
                customerAddress.AddressId == request.AddressId);

            newDefaultAddress.IsDefaultAddress = true;
            _context.CustomerAddresses.Update(newDefaultAddress);
        }

        /// <summary>
        ///     Remove default address where presently set. Shouldn't be on more than one but let's be sure
        /// </summary>
        /// <param name="request"></param>
        private void ClearExistingDefaultAddresses(SetAddressToDefaultCommand request)
        {
            foreach (var nonDefaultAddress in _context.CustomerAddresses.Where(customerAddress =>
                         customerAddress.CustomerId == request.CustomerId &&
                         customerAddress.AddressId != request.AddressId &&
                         customerAddress.IsDefaultAddress == true))
            {
                nonDefaultAddress.IsDefaultAddress = false;
                _context.CustomerAddresses.Update(nonDefaultAddress);
            }
        }
    }
}