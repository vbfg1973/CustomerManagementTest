using AutoMapper;
using CustomerManagement.Data;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Queries.Queries
{
    /// <summary>
    ///     Handler for simple id based customer lookup
    /// </summary>
    public class QueryCustomerByIdHandler : IRequestHandler<QueryCustomerById, CustomerWithAllDetailsResponse>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<QueryCustomerByIdHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public QueryCustomerByIdHandler(CustomerManagementContext context, IMapper mapper,
            ILogger<QueryCustomerByIdHandler> logger)
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
        public async Task<CustomerWithAllDetailsResponse> Handle(QueryCustomerById request,
            CancellationToken cancellationToken)
        {
            var customer = (await _mapper
                .ProjectTo<CustomerWithAllDetailsResponse>(_context.Customers.Where(x => x.Id == request.Id))
                .ToListAsync(cancellationToken)).FirstOrDefault();

            if (customer == null) throw new KeyNotFoundException();

            return customer!;
        }
    }
}