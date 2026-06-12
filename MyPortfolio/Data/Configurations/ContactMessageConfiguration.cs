using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Configurations
{
    public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
    {
        public void Configure(EntityTypeBuilder<ContactMessage> builder)
        {
            builder.ToTable("ContactMessages");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Subject)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Message)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(c => c.Priority)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.HasIndex(c => c.Status)
                .HasDatabaseName("IX_ContactMessages_Status");

            builder.HasIndex(c => c.IsRead)
                .HasDatabaseName("IX_ContactMessages_IsRead");

            builder.HasIndex(c => c.SentAt)
                .HasDatabaseName("IX_ContactMessages_SentAt");

            builder.HasIndex(c => c.Email)
                .HasDatabaseName("IX_ContactMessages_Email");

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
