using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Configurations
{
    public class BlogPostTagConfiguration : IEntityTypeConfiguration<BlogPostTag>
    {
        public void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.ToTable("BlogPostTags");

            builder.HasKey(bt => new { bt.BlogPostId, bt.TagId });

            builder.HasOne(bt => bt.BlogPost)
                .WithMany(b => b.BlogPostTags)
                .HasForeignKey(bt => bt.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogPostTags)
                .HasForeignKey(bt => bt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(bt => bt.BlogPostId)
                .HasDatabaseName("IX_BlogPostTags_BlogPostId");

            builder.HasIndex(bt => bt.TagId)
                .HasDatabaseName("IX_BlogPostTags_TagId");
        }
    }
}
