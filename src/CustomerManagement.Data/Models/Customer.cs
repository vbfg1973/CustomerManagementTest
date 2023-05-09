﻿namespace CustomerManagement.Data.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Middlenames { get; set; }
        public string Surname { get; set; } = null!;

        public List<Address> Addresses { get; set; } = new();
        public List<CustomerAddress> CustomerAddresses { get; set; } = new();
        public List<ContactDetail> ContactDetails { get; set; } = new();
    }
}