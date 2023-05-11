using CustomerManagement.Data.Models;

namespace CustomerManagement.Domain.Customers.Responses
{
    /// <summary>
    ///     Customer's contact details
    /// </summary>
    public class ContactDetailsResponseDto
    {
        /// <summary>
        ///     Unique identifier for contact details
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Contains the number, address, etc
        /// </summary>
        public string Detail { get; set; } = null!;

        /// <summary>
        ///     Indicated the type of contact detail
        /// </summary>
        public ContactDetailsType ContactType { get; set; }

        /// <summary>
        ///     Is this customers preferred point of contact
        /// </summary>
        public bool IsPreferred { get; set; }
    }
}