using MediatR;

namespace CustomerManagement.Domain.Customers.Commands
{
    /// <summary>
    ///     Create a basic customer
    /// </summary>
    public class CreateCustomerDto
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
        public string Middlenames { get; init; } = null!;

        /// <summary>
        ///     Customer's surname
        /// </summary>
        public string Surname { get; init; } = null!;

        /// <summary>
        ///     Customer's email address
        /// </summary>
        public string EMail { get; init; } = null!;
    }
    
    /// <summary>
    ///     Create a basic customer
    /// </summary>
    public class CreateCustomer : IRequest<Guid>
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
        public string Middlenames { get; init; } = null!;

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