using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Configurations
{
    public class ProjectTagConfiguration : IEntityTypeConfiguration<ProjectTag>
    {
        public void Configure(EntityTypeBuilder<ProjectTag> builder)
        {
            builder.ToTable("ProjectTags");

            builder.HasKey(pt => new { pt.ProjectId, pt.TagId });

            builder.HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTags)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Tag)
                .WithMany(t => t.ProjectTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pt => pt.ProjectId)
                .HasDatabaseName("IX_ProjectTags_ProjectId");

            builder.HasIndex(pt => pt.TagId)
                .HasDatabaseName("IX_ProjectTags_TagId");
        }
    }
}
