using CustomerManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Data.TypeConfiguration
{
    public class ContactDetailsTypeConfiguration : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            builder
                .HasOne<Customer>(contactDetails => contactDetails.Customer)
                .WithMany(customer => customer.ContactDetails)
                .HasForeignKey(contactDetails => contactDetails.CustomerId);

            builder
                .HasIndex(x => x.Detail)
                .IsUnique();
        }
    }
}