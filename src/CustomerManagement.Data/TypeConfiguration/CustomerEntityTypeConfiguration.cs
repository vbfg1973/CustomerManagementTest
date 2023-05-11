using CustomerManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Data.TypeConfiguration
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Many to many relationship between customers and addresses
            builder
                .HasMany(customer => customer.Addresses)
                .WithMany(address => address.Customers)
                .UsingEntity<CustomerAddress>(j => j.HasKey(k => new { k.CustomerId, k.AddressId }));

            // Composite key
            builder.HasIndex(x => new { x.FirstName, x.Surname });
        }
    }
}