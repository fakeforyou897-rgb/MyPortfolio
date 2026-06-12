using Microsoft.EntityFrameworkCore;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Seeds
{
    public static class TagSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var tags = new List<Tag>
            {
                new Tag { Id = Guid.NewGuid(), Name = "C#", Slug = "csharp".ToSlug(), Color = "#512BD4", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "ASP.NET Core", Slug = "aspnet-core".ToSlug(), Color = "#512BD4", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "JavaScript", Slug = "javascript".ToSlug(), Color = "#F7DF1E", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "TypeScript", Slug = "typescript".ToSlug(), Color = "#3178C6", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "React", Slug = "react".ToSlug(), Color = "#61DAFB", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Angular", Slug = "angular".ToSlug(), Color = "#DD0031", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Vue.js", Slug = "vuejs".ToSlug(), Color = "#42B883", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Node.js", Slug = "nodejs".ToSlug(), Color = "#339933", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Python", Slug = "python".ToSlug(), Color = "#3776AB", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Java", Slug = "java".ToSlug(), Color = "#007396", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "SQL Server", Slug = "sql-server".ToSlug(), Color = "#CC2927", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "PostgreSQL", Slug = "postgresql".ToSlug(), Color = "#336791", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "MongoDB", Slug = "mongodb".ToSlug(), Color = "#47A248", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Docker", Slug = "docker".ToSlug(), Color = "#2496ED", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Kubernetes", Slug = "kubernetes".ToSlug(), Color = "#326CE5", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Azure", Slug = "azure".ToSlug(), Color = "#0078D4", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "AWS", Slug = "aws".ToSlug(), Color = "#FF9900", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "Git", Slug = "git".ToSlug(), Color = "#F05032", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "REST API", Slug = "rest-api".ToSlug(), Color = "#009688", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Tag { Id = Guid.NewGuid(), Name = "GraphQL", Slug = "graphql".ToSlug(), Color = "#E10098", IsActive = true, CreatedAt = DateTime.UtcNow }
            };

            modelBuilder.Entity<Tag>().HasData(tags);
        }
    }
}
