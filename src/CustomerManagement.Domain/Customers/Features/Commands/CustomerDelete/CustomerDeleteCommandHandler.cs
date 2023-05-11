using AutoMapper;
using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Features.Commands.CustomerDelete
{
    /// <summary>
    ///     Handler for customer delete commands
    /// </summary>
    public class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommand>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<CustomerDeleteCommandHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///     ctor
        /// </summary>
        public CustomerDeleteCommandHandler(CustomerManagementContext context, IMapper mapper,
            ILogger<CustomerDeleteCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     Handle CustomerDeleteCommands
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message($"Attempting to delete customer identified by {request.Id}"),
                LogFmt.CorrelationId(request));

            // Get a 404 if customer doesn't exist
            if (!_context.Customers.Any(customer => customer.Id == request.Id))
                throw new ResourceNotFoundException(ExceptionMessages.CustomerDoesNotExist);

            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var customer = new Customer { Id = request.Id };
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message($"Deleted customer identified by {request.Id}"),
                LogFmt.CorrelationId(request));
        }
    }
}