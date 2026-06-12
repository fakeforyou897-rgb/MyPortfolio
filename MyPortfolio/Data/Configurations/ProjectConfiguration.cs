using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // Table name
            builder.ToTable("Projects");

            // Primary key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(p => p.ShortDescription)
                .HasMaxLength(500);

            builder.Property(p => p.Technologies)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(p => p.Priority)
                .HasConversion<string>()
                .HasMaxLength(50);

            // Indexes
            builder.HasIndex(p => p.Slug)
                .IsUnique()
                .HasDatabaseName("IX_Projects_Slug");

            builder.HasIndex(p => p.Status)
                .HasDatabaseName("IX_Projects_Status");

            builder.HasIndex(p => p.IsFeatured)
                .HasDatabaseName("IX_Projects_IsFeatured");

            builder.HasIndex(p => p.IsPublished)
                .HasDatabaseName("IX_Projects_IsPublished");

            builder.HasIndex(p => p.DisplayOrder)
                .HasDatabaseName("IX_Projects_DisplayOrder");

            builder.HasIndex(p => p.CategoryId)
                .HasDatabaseName("IX_Projects_CategoryId");

            builder.HasIndex(p => p.CreatedAt)
                .HasDatabaseName("IX_Projects_CreatedAt");

            // Relationships
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.ProjectTags)
                .WithOne(pt => pt.Project)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Query filter for soft delete
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
