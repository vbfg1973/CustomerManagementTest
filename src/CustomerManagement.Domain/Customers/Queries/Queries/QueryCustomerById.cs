using CustomerManagement.Domain.Customers.Responses;
using MediatR;

namespace CustomerManagement.Domain.Customers.Queries.Queries
{
    /// <summary>
    /// </summary>
    public class QueryCustomerById : IRequest<CustomerWithAllDetailsResponse>
    {
        /// <summary>
        ///     Customer's ID
        /// </summary>
        public Guid Id { get; set; }
    }
}