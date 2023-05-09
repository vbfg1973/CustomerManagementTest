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
                .UsingEntity<CustomerAddress>(
                    right => right
                        .HasOne(customerAddress => customerAddress.Address)
                        .WithMany(customer => customer.CustomerAddresses)
                        .HasForeignKey(customerAddress => customerAddress.AddressId),
                    left => left
                        .HasOne(customerAddress => customerAddress.Customer)
                        .WithMany(address => address.CustomerAddresses)
                        .HasForeignKey(customerAddress => customerAddress.CustomerId),
                    joinTable =>
                    {
                        joinTable
                            .Property(customerAddress => customerAddress.IsDefaultAddress);

                        joinTable
                            .HasKey(customerAddress => new { customerAddress.CustomerId, customerAddress.AddressId });
                    }
                );


            // Composite key
            builder.HasIndex(x => new { x.FirstName, x.Surname });
        }
    }
}