using AutoMapper;
using CustomerManagement.Common.Logging;
using CustomerManagement.Data;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Customers.Responses;
using CustomerManagement.Domain.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Domain.Customers.Queries.Queries
{
    /// <summary>
    ///     Handler for simple id based customer lookup
    /// </summary>
    public class
        CustomerByPagesQueryHandler : IRequestHandler<CustomersByPagesQuery,
            PagedList<CustomerWithAllDetailsResponse>>
    {
        private readonly CustomerManagementContext _context;
        private readonly ILogger<CustomerByPagesQueryHandler> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public CustomerByPagesQueryHandler(CustomerManagementContext context, IMapper mapper,
            ILogger<CustomerByPagesQueryHandler> logger)
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
        public async Task<PagedList<CustomerWithAllDetailsResponse>> Handle(CustomersByPagesQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Querying for customers"),
                LogFmt.CorrelationId(request.CorrelationId!));

            IQueryable<Customer> queryable = _context.Customers.Include(x => x.Addresses);

            queryable = AugmentQueryWithSurname(request, queryable);

            queryable = AugmentQueryWithEmail(request, queryable);

            queryable = AugmentQueryWithPostalTown(request, queryable);

            queryable = AugmentQueryWithPostCode(request, queryable);

            queryable = queryable.OrderBy(x => x.Surname).ThenBy(x => x.FirstName);

            _logger.LogDebug("{ResultCount} {CorrelationId}",
                LogFmt.ResultCount(_mapper.ProjectTo<CustomerWithAllDetailsResponse>(queryable).Count()),
                LogFmt.CorrelationId(request.CorrelationId!));

            return await PagedList<CustomerWithAllDetailsResponse>.ToPagedList(
                _mapper.ProjectTo<CustomerWithAllDetailsResponse>(queryable), request);
        }

        /// <summary>
        ///     Adds a surname stipulation if present on query object
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private IQueryable<Customer> AugmentQueryWithSurname(CustomersByPagesQuery request,
            IQueryable<Customer> queryable)
        {
            if (!string.IsNullOrEmpty(request.Surname))
            {
                _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding surname stipulation to query"),
                    LogFmt.CorrelationId(request.CorrelationId!));

                queryable = queryable.Where(x => x.Surname == request.Surname);
            }

            return queryable;
        }

        /// <summary>
        ///     Adds an email stipulation if present on query object
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private IQueryable<Customer> AugmentQueryWithEmail(CustomersByPagesQuery request,
            IQueryable<Customer> queryable)
        {
            if (string.IsNullOrEmpty(request.EMail)) return queryable;

            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding email stipulation to query"),
                LogFmt.CorrelationId(request.CorrelationId!));

            queryable = queryable.Where(customer =>
                customer
                    .ContactDetails
                    .Any(contactDetail =>
                        contactDetail.Detail == request.EMail &&
                        contactDetail.ContactType == ContactDetailsType.Email));

            return queryable;
        }

        /// <summary>
        ///     Adds a postal town stipulation if present on query object
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private IQueryable<Customer> AugmentQueryWithPostalTown(CustomersByPagesQuery request,
            IQueryable<Customer> queryable)
        {
            if (string.IsNullOrEmpty(request.PostalTown)) return queryable;

            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding postal town stipulation to query"),
                LogFmt.CorrelationId(request.CorrelationId!));

            queryable = queryable.Where(customer =>
                customer
                    .Addresses
                    .Any(address =>
                        address.PostalTown == request.PostalTown));

            return queryable;
        }

        /// <summary>
        ///     Adds a post code stipulation if present on query object
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private IQueryable<Customer> AugmentQueryWithPostCode(CustomersByPagesQuery request,
            IQueryable<Customer> queryable)
        {
            if (string.IsNullOrEmpty(request.PostCode)) return queryable;

            _logger.LogDebug("{Message} {CorrelationId}", LogFmt.Message("Adding post code stipulation to query"),
                LogFmt.CorrelationId(request.CorrelationId!));

            queryable = queryable.Where(customer =>
                customer
                    .Addresses
                    .Any(address =>
                        address.PostCode == request.PostCode));

            return queryable;
        }
    }
}