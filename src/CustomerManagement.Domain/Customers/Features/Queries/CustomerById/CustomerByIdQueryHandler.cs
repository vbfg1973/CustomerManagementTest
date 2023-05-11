using AutoMapper;
using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Features.Queries.CustomerById
{
    /// <summary>
    ///     Handler for simple id based customer lookup
    /// </summary>
    public class CustomerByIdQueryHandler : IRequestHandler<ByIdQuery, CustomerWithAllDetailsResponseDto>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<CustomerByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public CustomerByIdQueryHandler(CustomerManagementContext context, IMapper mapper,
            ILogger<CustomerByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     Request Handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<CustomerWithAllDetailsResponseDto> Handle(ByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Getting a single customer by their ID"),
                LogFmt.CorrelationId(request.CorrelationId));

            var customer = (await _mapper
                .ProjectTo<CustomerWithAllDetailsResponseDto>(_context.Customers.Where(x => x.Id == request.Id))
                .ToListAsync(cancellationToken)).FirstOrDefault();

            if (customer == null) throw new KeyNotFoundException();

            return customer!;
        }
    }
}