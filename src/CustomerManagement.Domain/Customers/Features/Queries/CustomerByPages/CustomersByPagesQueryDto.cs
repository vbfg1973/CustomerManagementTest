using CustomerManagement.Domain.Paging;

namespace CustomerManagement.Domain.Customers.Features.Queries.CustomerByPages
{
    /// <summary>
    ///     Dto object for customer queries
    /// </summary>
    public class CustomersByPagesQueryDto : BasePagedQuery
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
    }
}