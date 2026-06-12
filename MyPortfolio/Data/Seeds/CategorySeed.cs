using Microsoft.EntityFrameworkCore;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Seeds
{
    /// <summary>
    /// Seeder for Category data
    /// </summary>
    public class CategorySeed : ISeeder
    {
        public int Order => 1; // Execute first

        public void Seed(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Web Development",
                    Slug = "web-development".ToSlug(),
                    Description = "Web development projects and articles",
                    Color = "#3B82F6",
                    Icon = "fa-globe",
                    DisplayOrder = 1,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Mobile Development",
                    Slug = "mobile-development".ToSlug(),
                    Description = "Mobile app development",
                    Color = "#10B981",
                    Icon = "fa-mobile",
                    DisplayOrder = 2,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Desktop Applications",
                    Slug = "desktop-applications".ToSlug(),
                    Description = "Desktop software development",
                    Color = "#8B5CF6",
                    Icon = "fa-desktop",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "DevOps & Cloud",
                    Slug = "devops-cloud".ToSlug(),
                    Description = "DevOps, CI/CD, and cloud infrastructure",
                    Color = "#F59E0B",
                    Icon = "fa-cloud",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Programming Tutorials",
                    Slug = "programming-tutorials".ToSlug(),
                    Description = "Programming guides and tutorials",
                    Color = "#EF4444",
                    Icon = "fa-code",
                    DisplayOrder = 5,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}