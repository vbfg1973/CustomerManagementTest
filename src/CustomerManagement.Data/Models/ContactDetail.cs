namespace CustomerManagement.Data.Models
{
    public class ContactDetail
    {
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

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}