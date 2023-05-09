namespace CustomerManagement.Data.Models
{
    public class CustomerAddress
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public bool IsDefaultAddress { get; set; }
    }
}