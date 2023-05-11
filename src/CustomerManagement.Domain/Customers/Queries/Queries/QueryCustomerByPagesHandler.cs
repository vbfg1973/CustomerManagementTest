using AutoMapper;
using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Queries.Queries
{
    /// <summary>
    ///     Handler for simple id based customer lookup
    /// </summary>
    public class
        QueryCustomerByPagesHandler : IRequestHandler<QueryCustomersByPages,
            IEnumerable<CustomerWithAllDetailsResponse>>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<QueryCustomerByPagesHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public QueryCustomerByPagesHandler(CustomerManagementContext context, IMapper mapper,
            ILogger<QueryCustomerByPagesHandler> logger)
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
        public async Task<IEnumerable<CustomerWithAllDetailsResponse>> Handle(QueryCustomersByPages request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Querying for customers"),
                LogFmt.CorrelationId(request.CorrelationId));

            IQueryable<Customer> queryable = _context.Customers.Include(x => x.Addresses);

            queryable = AugmentQueryWithSurname(request, queryable);

            queryable = AugmentQueryWithEmail(request, queryable);

            queryable = AugmentQueryWithPostalTown(request, queryable);

            queryable = AugmentQueryWithPostCode(request, queryable);

            _logger.LogDebug("{ResultCount} {CorrelationId}",
                LogFmt.ResultCount(_mapper.ProjectTo<CustomerWithAllDetailsResponse>(queryable).Count()),
                LogFmt.CorrelationId(request.CorrelationId));

            return await _mapper.ProjectTo<CustomerWithAllDetailsResponse>(queryable)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Customer> AugmentQueryWithSurname(QueryCustomersByPages request,
            IQueryable<Customer> queryable)
        {
            if (!string.IsNullOrEmpty(request.Surname))
            {
                _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding surname stipulation to query"),
                    LogFmt.CorrelationId(request.CorrelationId));

                queryable = queryable.Where(x => x.Surname == request.Surname);
            }

            return queryable;
        }

        private IQueryable<Customer> AugmentQueryWithEmail(QueryCustomersByPages request,
            IQueryable<Customer> queryable)
        {
            if (string.IsNullOrEmpty(request.EMail)) return queryable;
            
            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding email stipulation to query"),
                LogFmt.CorrelationId(request.CorrelationId));

            queryable = queryable.Where(customer =>
                customer
                    .ContactDetails
                    .Any(contactDetail =>
                        contactDetail.Detail == request.EMail &&
                        contactDetail.ContactType == ContactDetailsType.Email));

            return queryable;
        }

        private IQueryable<Customer> AugmentQueryWithPostalTown(QueryCustomersByPages request,
            IQueryable<Customer> queryable)
        {
            if (string.IsNullOrEmpty(request.PostalTown)) return queryable;
            
            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding postal town stipulation to query"),
                LogFmt.CorrelationId(request.CorrelationId));

            queryable = queryable.Where(customer =>
                customer
                    .Addresses
                    .Any(address =>
                        address.PostalTown == request.PostalTown));

            return queryable;
        }

        private IQueryable<Customer> AugmentQueryWithPostCode(QueryCustomersByPages request,
            IQueryable<Customer> queryable)
        {
            if (string.IsNullOrEmpty(request.PostCode)) return queryable;
            
            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding post code stipulation to query"),
                LogFmt.CorrelationId(request.CorrelationId));

            queryable = queryable.Where(customer =>
                customer
                    .Addresses
                    .Any(address =>
                        address.PostCode == request.PostCode));

            return queryable;
        }
    }
}