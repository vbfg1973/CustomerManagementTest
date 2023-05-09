namespace CustomerManagement.Data.Models
{
    /// <summary>
    ///     Standard UK address format. Premise details assumed encapsulated in address lines.
    /// </summary>
    public class Address
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     First Address Line. Often contains premise and thoroughfare information. Never empty
        /// </summary>
        public string AddressLine1 { get; set; } = null!;

        /// <summary>
        ///     Second Address Line. Often contains thoroughfare and locality information. May be empty
        /// </summary>
        public string? AddressLine2 { get; set; }

        /// <summary>
        ///     Third address line. May be empty
        /// </summary>
        public string? AddressLine3 { get; set; }

        /// <summary>
        ///     The postal town. Never empty.
        /// </summary>
        public string PostalTown { get; set; } = null!;

        /// <summary>
        ///     The postal county. No longer required or even existing for some addresses.
        /// </summary>
        public string? County { get; set; }

        /// <summary>
        ///     Correctly formatted postcode. Capitalised and spaced. Never empty
        /// </summary>
        public string PostCode { get; set; } = null!;

        public List<Customer> Customers { get; set; } = new();
        // public List<CustomerAddress> CustomerAddresses { get; set; } = new();
    }
}