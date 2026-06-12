using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(80);

            builder.HasIndex(t => t.Slug)
                .IsUnique()
                .HasDatabaseName("IX_Tags_Slug");

            builder.HasIndex(t => t.IsActive)
                .HasDatabaseName("IX_Tags_IsActive");

            builder.HasIndex(t => t.UsageCount)
                .HasDatabaseName("IX_Tags_UsageCount");

            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}
