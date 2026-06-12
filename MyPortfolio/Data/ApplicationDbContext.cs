using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Common;

namespace MyPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure soft delete global query filter
            modelBuilder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<BlogPost>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<ContactMessage>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Tag>().HasQueryFilter(t => !t.IsDeleted);

            // Configure many-to-many relationships
            modelBuilder.Entity<BlogPostTag>()
                .HasKey(bt => new { bt.BlogPostId, bt.TagId });

            modelBuilder.Entity<BlogPostTag>()
                .HasOne(bt => bt.BlogPost)
                .WithMany(b => b.BlogPostTags)
                .HasForeignKey(bt => bt.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BlogPostTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogPostTags)
                .HasForeignKey(bt => bt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTag>()
                .HasKey(pt => new { pt.ProjectId, pt.TagId });

            modelBuilder.Entity<ProjectTag>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTags)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProjectTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Category self-referencing relationship
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes for better performance
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Slug)
                .IsUnique();
            
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Status);
            
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.IsFeatured);
            
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.IsPublished);
            
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.DisplayOrder);

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.CategoryId);

            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.Slug)
                .IsUnique();
            
            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.Status);
            
            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.PublishedAt);
            
            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.IsFeatured);

            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.IsPublished);

            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.CategoryId);

            modelBuilder.Entity<ContactMessage>()
                .HasIndex(c => c.Status);
            
            modelBuilder.Entity<ContactMessage>()
                .HasIndex(c => c.IsRead);
            
            modelBuilder.Entity<ContactMessage>()
                .HasIndex(c => c.SentAt);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.IsActive);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.ParentCategoryId);

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Slug)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.IsActive);

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.UsageCount);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && 
                           (e.State == EntityState.Added || 
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Implement soft delete
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    entity.DeletedAt = now;
                }
            }
        }
    }
}
