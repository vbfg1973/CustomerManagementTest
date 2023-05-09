using CustomerManagement.Data.Models;
using CustomerManagement.Data.Support;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data
{
    public class CustomerManagementContext : DbContext
    {
        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<ContactDetail> ContactDetails { get; set; } = null!;

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Missed this for hours!
            modelBuilder.ApplyConfigurationsFromAssembly(DataAssemblyReference.Assembly);

            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer
                    {
                        Id = Guid.Parse("e5eafda2-c5d9-46b5-b364-95bb6da27b58"),
                        Title = "Mr",
                        FirstName = "Darcy",
                        Surname = "Carter",
                    });

            modelBuilder.Entity<ContactDetail>()
                .HasData(
                    new ContactDetail()
                    {
                        Id = Guid.Parse("f5eea8e5-accf-4593-bcd9-016a897cd0e1"),
                        CustomerId = Guid.Parse("e5eafda2-c5d9-46b5-b364-95bb6da27b58"),
                        Detail = "dcarter.customermanagement@gmail.com",
                        ContactType = ContactDetailsType.Email,
                        IsPreferred = true
                    },
                    new ContactDetail()
                    {
                        Id = Guid.Parse("de920a0d-9453-487b-9c65-4ee865bd417f"),
                        CustomerId = Guid.Parse("e5eafda2-c5d9-46b5-b364-95bb6da27b58"),
                        Detail = "0123456789",
                        ContactType = ContactDetailsType.Landline,
                        IsPreferred = false
                    }
                );
            
            modelBuilder.Entity<Address>()
                .HasData(
                    new Address()
                    {
                        Id = Guid.Parse("38ed5f3f-3460-40ba-8009-93371fefdfa2"),
                        AddressLine1 = "22 Oil Drum Lane",
                        PostalTown = "East Cheam",
                        PostCode = "W1A 1AA"
                    },
                    new Address()
                    {
                        Id = Guid.Parse("7242a930-fc47-4244-a97d-52b515aea1e4"),
                        AddressLine1 = "22 Acacia Avenue",
                        PostalTown = "Salford",
                        PostCode = "M60 1AA"
                    }
                );

            modelBuilder.Entity<CustomerAddress>()
                .HasData(
                    new CustomerAddress()
                    {
                        CustomerId = Guid.Parse("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), // Darcy Carter
                        AddressId = Guid.Parse("38ed5f3f-3460-40ba-8009-93371fefdfa2"),  // Oil Drum Lane
                        IsDefaultAddress = true
                    },
                    new CustomerAddress()
                    {
                        CustomerId = Guid.Parse("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), // Darcy Carter
                        AddressId = Guid.Parse("7242a930-fc47-4244-a97d-52b515aea1e4"),  // Acacia Avenue
                        IsDefaultAddress = false
                    }
                );
        }
    }
}