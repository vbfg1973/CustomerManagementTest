namespace CustomerManagement.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            ContactDetails = new HashSet<ContactDetails>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Middlenames { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public ICollection<Address> Addresses { get; set; }
        public List<CustomerAddress> CustomerAddresses { get; set; }

        public ICollection<ContactDetails> ContactDetails { get; set; }
    }
}