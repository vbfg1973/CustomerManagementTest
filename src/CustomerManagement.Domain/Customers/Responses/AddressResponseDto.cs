namespace CustomerManagement.Domain.Customers.Responses
{
    /// <summary>
    ///     A customer address
    /// </summary>
    public class AddressResponseDto
    {
        /// <summary>
        ///     Unique identifier for customer's address
        /// </summary>
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
    }
}