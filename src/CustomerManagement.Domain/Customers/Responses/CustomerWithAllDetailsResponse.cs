namespace CustomerManagement.Domain.Customers.Responses
{
    /// <summary>
    ///     A customer object containing all customer details
    /// </summary>
    public class CustomerWithAllDetailsResponse
    {
        /// <summary>
        ///     Unique identifier for customer
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Customer's personal title
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        ///     Customer's first name
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        ///     Customer's middle names if applicable
        /// </summary>
        public string? Middlenames { get; set; }

        /// <summary>
        ///     Customer's surname
        /// </summary>
        public string Surname { get; set; } = null!;

        /// <summary>
        ///     All customer contact avenues
        /// </summary>
        public List<ContactDetailsResponse> ContactDetails { get; set; }

        /// <summary>
        ///     All customer addresses
        /// </summary>
        public List<AddressResponse> Addresses { get; set; }
    }
}