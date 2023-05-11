namespace CustomerManagement.Domain.Abstract
{
    /// <summary>
    ///     Interface for customer objects
    /// </summary>
    public interface ICustomerRequest
    {
        /// <summary>
        ///     Customer's personal title
        /// </summary>
        string Title { get; init; }

        /// <summary>
        ///     Customer's first name
        /// </summary>
        string FirstName { get; init; }

        /// <summary>
        ///     Customers middle names if applicable
        /// </summary>
        string? Middlenames { get; init; }

        /// <summary>
        ///     Customer's surname
        /// </summary>
        string Surname { get; init; }

        /// <summary>
        ///     Customer's email address
        /// </summary>
        string EMail { get; init; }
    }
}