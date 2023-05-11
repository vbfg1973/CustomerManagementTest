using AutoMapper;
using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Abstract;
using CustomerManagement.Domain.Customers.Features.Queries.CustomerById;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Features.Commands.CustomerCreate
{
    /// <summary>
    ///     Handler for customer creation commands
    /// </summary>
    public class
        CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, CustomerWithAllDetailsResponseDto>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<CustomerCreateCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public CustomerCreateCommandHandler(CustomerManagementContext context, IMediator mediator, IMapper mapper,
            ILogger<CustomerCreateCommandHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     Request handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>id</returns>
        public async Task<CustomerWithAllDetailsResponseDto> Handle(CustomerCreateCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message($"Attempting to create customer with initials \"{request.CustomerInitials()}\""),
                LogFmt.CorrelationId(request.CorrelationId));

            // Prefer transactions to say a Unit of Work and generic repositories. Assists with things like 
            // an outbox pattern, where the event is also written to the DB as part of the same transaction,
            // for generating events we can guarantee will be sent
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var customer = _mapper.Map<CustomerCreateCommand, Customer>(request);
            customer.Id = Guid.NewGuid();
            _context.Customers.Add(customer);

            var contactDetails = request.CreateInitialCustomerContactDetail(customer.Id);
            _context.ContactDetails.Add(contactDetails);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var customerFull =
                await _mediator.Send(new CustomerByIdQuery { Id = customer.Id, CorrelationId = request.CorrelationId },
                    cancellationToken);

            _logger.LogDebug("{Message} {CorrelationId}",
                LogFmt.Message($"Customer {request.CustomerInitials()} created with id {customer.Id}"),
                LogFmt.CorrelationId(request.CorrelationId));

            return customerFull;
        }


    }
}