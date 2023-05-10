namespace CustomerManagement.Data.Models
{
    public class Customer
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
        ///     Customer's addresses
        /// </summary>
        public List<Address> Addresses { get; set; } = new();

        /// <summary>
        ///     Join tables to customer address. Default address flag is set here.
        /// </summary>
        public List<CustomerAddress> CustomerAddresses { get; set; } = new();

        /// <summary>
        ///     Customer's contact details
        /// </summary>
        public List<ContactDetail> ContactDetails { get; set; } = new();
    }
}