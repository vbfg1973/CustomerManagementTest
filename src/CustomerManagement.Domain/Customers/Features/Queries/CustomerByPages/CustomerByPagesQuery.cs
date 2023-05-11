using CustomerManagement.Common.Abstract;
using CustomerManagement.Domain.Customers.Responses;
using CustomerManagement.Domain.Paging;
using MediatR;

namespace CustomerManagement.Domain.Customers.Features.Queries.CustomerByPages
{
    /// <summary>
    ///     Paged queries for customers
    /// </summary>
    public class CustomerByPagesQuery : BasePagedQuery, IRequest<PagedList<CustomerWithAllDetailsResponseDto>>,
        ITrackableRequest
    {
        /// <summary>
        ///     Limit result to customers with a matching email
        /// </summary>
        public string? EMail { get; init; }

        /// <summary>
        ///     Limit results to customers with a matching Surname
        /// </summary>
        public string? Surname { get; init; }

        /// <summary>
        ///     Limit to customers with an address in this postal town
        /// </summary>
        public string? PostalTown { get; init; }

        /// <summary>
        ///     Limit results to customers with an address in this postcode
        /// </summary>
        public string? PostCode { get; init; }

        /// <summary>
        ///     CorrelationId
        /// </summary>
        public string CorrelationId { get; set; } = null!;
    }
}