using CustomerManagement.Data.Models;
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
        public virtual DbSet<ContactDetails> ContactDetails { get; set; } = null!;

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
    }
}