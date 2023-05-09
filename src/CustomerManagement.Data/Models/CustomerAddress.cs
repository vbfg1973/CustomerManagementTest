namespace CustomerManagement.Data.Models
{
    public class CustomerAddress
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public Guid AddressId { get; set; }
        public Address Address { get; set; } = null!;

        public bool IsDefaultAddress { get; set; }
    }
}