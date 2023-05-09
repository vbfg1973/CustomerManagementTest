using CustomerManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManagement.Data.TypeConfiguration
{
    public class ContactDetailsTypeConfiguration : IEntityTypeConfiguration<ContactDetails>
    {
        public void Configure(EntityTypeBuilder<ContactDetails> builder)
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