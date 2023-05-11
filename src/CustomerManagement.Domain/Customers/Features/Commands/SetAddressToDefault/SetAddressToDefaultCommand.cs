using CustomerManagement.Common.Abstract;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;

namespace CustomerManagement.Domain.Customers.Features.Commands.SetAddressToDefault
{
    /// <summary>
    ///     Set a customer's address to be the default
    /// </summary>
    public sealed class SetAddressToDefaultCommand : IRequest<CustomerWithAllDetailsResponseDto>, ITrackableRequest
    {
        /// <summary>
        ///     The unique identifier for the customer
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        ///     The unique identifier for the address
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        ///     Correlation id for tracking requests
        /// </summary>
        public string CorrelationId { get; set; } = null!;
    }
}