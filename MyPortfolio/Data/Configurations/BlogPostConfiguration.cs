using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("BlogPosts");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Slug)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(b => b.Content)
                .IsRequired();

            builder.Property(b => b.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.HasIndex(b => b.Slug)
                .IsUnique()
                .HasDatabaseName("IX_BlogPosts_Slug");

            builder.HasIndex(b => b.Status)
                .HasDatabaseName("IX_BlogPosts_Status");

            builder.HasIndex(b => b.PublishedAt)
                .HasDatabaseName("IX_BlogPosts_PublishedAt");

            builder.HasIndex(b => b.IsFeatured)
                .HasDatabaseName("IX_BlogPosts_IsFeatured");

            builder.HasIndex(b => b.IsPublished)
                .HasDatabaseName("IX_BlogPosts_IsPublished");

            builder.HasIndex(b => b.CategoryId)
                .HasDatabaseName("IX_BlogPosts_CategoryId");

            builder.HasIndex(b => b.CreatedAt)
                .HasDatabaseName("IX_BlogPosts_CreatedAt");

            builder.HasOne(b => b.Category)
                .WithMany(c => c.BlogPosts)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(b => b.BlogPostTags)
                .WithOne(bt => bt.BlogPost)
                .HasForeignKey(bt => bt.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
