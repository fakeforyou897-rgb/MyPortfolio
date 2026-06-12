using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Seeds
{
    public class TagSeed : ISeeder
    {
        public int Order => 2;

        public void Seed(ModelBuilder modelBuilder)
        {
            var createdAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var tags = new List<Tag>
            {
                new Tag { Id = Guid.Parse("a0000001-0000-0000-0000-000000000000"), Name = "C#", Slug = "csharp", Color = "#512BD4", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000002-0000-0000-0000-000000000000"), Name = "ASP.NET Core", Slug = "aspnet-core", Color = "#512BD4", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000003-0000-0000-0000-000000000000"), Name = "JavaScript", Slug = "javascript", Color = "#F7DF1E", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000004-0000-0000-0000-000000000000"), Name = "TypeScript", Slug = "typescript", Color = "#3178C6", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000005-0000-0000-0000-000000000000"), Name = "React", Slug = "react", Color = "#61DAFB", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000006-0000-0000-0000-000000000000"), Name = "Angular", Slug = "angular", Color = "#DD0031", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000007-0000-0000-0000-000000000000"), Name = "Vue.js", Slug = "vuejs", Color = "#42B883", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000008-0000-0000-0000-000000000000"), Name = "Node.js", Slug = "nodejs", Color = "#339933", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000009-0000-0000-0000-000000000000"), Name = "Python", Slug = "python", Color = "#3776AB", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000010-0000-0000-0000-000000000000"), Name = "Java", Slug = "java", Color = "#007396", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000011-0000-0000-0000-000000000000"), Name = "SQL Server", Slug = "sql-server", Color = "#CC2927", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000012-0000-0000-0000-000000000000"), Name = "PostgreSQL", Slug = "postgresql", Color = "#336791", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000013-0000-0000-0000-000000000000"), Name = "MongoDB", Slug = "mongodb", Color = "#47A248", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000014-0000-0000-0000-000000000000"), Name = "Docker", Slug = "docker", Color = "#2496ED", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000015-0000-0000-0000-000000000000"), Name = "Kubernetes", Slug = "kubernetes", Color = "#326CE5", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000016-0000-0000-0000-000000000000"), Name = "Azure", Slug = "azure", Color = "#0078D4", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000017-0000-0000-0000-000000000000"), Name = "AWS", Slug = "aws", Color = "#FF9900", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000018-0000-0000-0000-000000000000"), Name = "Git", Slug = "git", Color = "#F05032", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000019-0000-0000-0000-000000000000"), Name = "REST API", Slug = "rest-api", Color = "#009688", IsActive = true, CreatedAt = createdAt },
                new Tag { Id = Guid.Parse("a0000020-0000-0000-0000-000000000000"), Name = "GraphQL", Slug = "graphql", Color = "#E10098", IsActive = true, CreatedAt = createdAt }
            };

            modelBuilder.Entity<Tag>().HasData(tags);
        }
    }
}
