using CustomerManagement.Domain.Abstract;
using CustomerManagement.Domain.Customers.Responses;
using MediatR;

namespace CustomerManagement.Domain.Customers.Features.Commands.CustomerCreate
{
    /// <summary>
    ///     Create a basic customer
    /// </summary>
    public class CustomerCreateCommand : IRequest<CustomerWithAllDetailsResponseDto>, ICustomerRequest, ITrackableRequest
    {
        /// <summary>
        ///     Customer's personal title
        /// </summary>
        public string Title { get; init; } = null!;

        /// <summary>
        ///     Customer's first name
        /// </summary>
        public string FirstName { get; init; } = null!;

        /// <summary>
        ///     Customers middle names if applicable
        /// </summary>
        public string? Middlenames { get; init; }

        /// <summary>
        ///     Customer's surname
        /// </summary>
        public string Surname { get; init; } = null!;

        /// <summary>
        ///     Customer's email address
        /// </summary>
        public string EMail { get; init; } = null!;

        /// <summary>
        ///     Request Correlation Id
        /// </summary>
        public string CorrelationId { get; set; } = null!;
    }
}