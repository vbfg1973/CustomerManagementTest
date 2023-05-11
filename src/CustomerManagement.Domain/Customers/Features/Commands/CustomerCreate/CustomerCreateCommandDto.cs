using CustomerManagement.Domain.Abstract;

namespace CustomerManagement.Domain.Customers.Features.Commands.CustomerCreate
{
    /// <summary>
    ///     Create a basic customer
    /// </summary>
    public class CustomerCreateCommandDto : ICustomerRequest
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
        public string? Middlenames { get; init; } = null!;

        /// <summary>
        ///     Customer's surname
        /// </summary>
        public string Surname { get; init; } = null!;

        /// <summary>
        ///     Customer's email address
        /// </summary>
        public string EMail { get; init; } = null!;
    }
}