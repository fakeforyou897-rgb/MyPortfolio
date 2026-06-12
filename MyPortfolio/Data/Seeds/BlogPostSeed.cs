using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Seeds
{
    public class BlogPostSeed : ISeeder
    {
        public int Order => 4;

        public void Seed(ModelBuilder modelBuilder)
        {
            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Title = "Getting Started with ASP.NET Core 8",
                    Slug = "getting-started-aspnet-core-8",
                    Content = "ASP.NET Core 8 brings significant improvements and new features. This guide covers setup, your first API, and best practices.",
                    Summary = "A comprehensive guide to getting started with ASP.NET Core 8 with practical examples.",
                    Author = "Mostafa Said",
                    ImageUrl = "https://via.placeholder.com/400x300?text=ASP.NET+Core+8",
                    FeaturedImageUrl = "https://via.placeholder.com/800x400?text=ASP.NET+Core+8",
                    Status = BlogPostStatus.Published,
                    PublishedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                    ViewCount = 523,
                    LikeCount = 87,
                    CommentCount = 12,
                    ShareCount = 34,
                    ReadingTimeMinutes = 8,
                    AllowComments = true,
                    IsFeatured = true,
                    IsPublished = true,
                    MetaTitle = "Getting Started with ASP.NET Core 8 - Complete Guide",
                    MetaDescription = "Learn how to get started with ASP.NET Core 8 with step-by-step examples and best practices.",
                    MetaKeywords = "ASP.NET Core 8, C#, Web Development, Tutorial",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new BlogPost
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Title = "React Performance Optimization Techniques",
                    Slug = "react-performance-optimization",
                    Content = "React performance is crucial for user experience. This article covers code splitting, memoization, component optimization, and bundle analysis.",
                    Summary = "Essential techniques to optimize your React applications for better performance.",
                    Author = "Mostafa Said",
                    ImageUrl = "https://via.placeholder.com/400x300?text=React+Performance",
                    FeaturedImageUrl = "https://via.placeholder.com/800x400?text=React+Performance",
                    Status = BlogPostStatus.Published,
                    PublishedAt = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                    ViewCount = 412,
                    LikeCount = 76,
                    CommentCount = 8,
                    ShareCount = 25,
                    ReadingTimeMinutes = 6,
                    AllowComments = true,
                    IsFeatured = true,
                    IsPublished = true,
                    MetaTitle = "React Performance Optimization - Best Practices",
                    MetaDescription = "Learn how to optimize React applications with code splitting, memoization, and more.",
                    MetaKeywords = "React, Performance, Optimization, JavaScript",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new BlogPost
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    Title = "Docker and Kubernetes for Beginners",
                    Slug = "docker-kubernetes-beginners",
                    Content = "Containerization is essential in modern development. This guide covers Docker basics, Kubernetes essentials, and best practices.",
                    Summary = "A beginner-friendly introduction to Docker and Kubernetes for containerized applications.",
                    Author = "Mostafa Said",
                    ImageUrl = "https://via.placeholder.com/400x300?text=Docker+Kubernetes",
                    FeaturedImageUrl = "https://via.placeholder.com/800x400?text=Docker+Kubernetes",
                    Status = BlogPostStatus.Published,
                    PublishedAt = new DateTime(2024, 12, 22, 0, 0, 0, DateTimeKind.Utc),
                    ViewCount = 298,
                    LikeCount = 54,
                    CommentCount = 5,
                    ShareCount = 18,
                    ReadingTimeMinutes = 10,
                    AllowComments = true,
                    IsFeatured = false,
                    IsPublished = true,
                    MetaTitle = "Docker and Kubernetes for Beginners - Complete Guide",
                    MetaDescription = "Learn Docker and Kubernetes with practical examples for containerized development.",
                    MetaKeywords = "Docker, Kubernetes, Containers, DevOps",
                    CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    CreatedAt = new DateTime(2024, 12, 22, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 12, 24, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            modelBuilder.Entity<BlogPost>().HasData(blogPosts);
        }
    }
}
