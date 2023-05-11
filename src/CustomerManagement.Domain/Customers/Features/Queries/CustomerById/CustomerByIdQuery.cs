using CustomerManagement.Domain.Abstract;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;

namespace CustomerManagement.Domain.Customers.Features.Queries.CustomerById
{
    /// <summary>
    /// </summary>
    public class CustomerByIdQuery : IRequest<CustomerWithAllDetailsResponseDto>, ITrackableRequest
    {
        /// <summary>
        ///     Customer's ID
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        ///     CorrelationId
        /// </summary>
        public string CorrelationId { get; set; } = null!;
    }
}