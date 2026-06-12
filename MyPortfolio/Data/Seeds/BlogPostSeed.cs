using Microsoft.EntityFrameworkCore;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Seeds
{
    /// <summary>
    /// Seeder for sample BlogPost data
    /// </summary>
    public class BlogPostSeed : ISeeder
    {
        public int Order => 4; // Execute after projects

        public void Seed(ModelBuilder modelBuilder)
        {
            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Title = "Getting Started with ASP.NET Core 8",
                    Slug = "getting-started-aspnet-core-8".ToSlug(),
                    Content = @"ASP.NET Core 8 brings significant improvements and new features. In this comprehensive guide, we'll explore:

1. **What's New in .NET 8**
   - Performance improvements
   - New language features
   - Enhanced tooling

2. **Setting Up Your Environment**
   - Installing .NET 8 SDK
   - Creating your first project
   - Running and testing

3. **Building Your First API**
   - Creating a minimal API
   - Adding middleware
   - Handling requests

4. **Best Practices**
   - Error handling
   - Logging
   - Security

This tutorial will help you get productive quickly with the latest version of ASP.NET Core.",
                    Summary = "A comprehensive guide to getting started with ASP.NET Core 8 with practical examples.",
                    Author = "Mostafa Said",
                    ImageUrl = "https://via.placeholder.com/400x300?text=ASP.NET+Core+8",
                    FeaturedImageUrl = "https://via.placeholder.com/800x400?text=ASP.NET+Core+8",
                    Status = BlogPostStatus.Published,
                    PublishedAt = DateTime.UtcNow.AddMonths(-2),
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
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1)
                },
                new BlogPost
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Title = "React Performance Optimization Techniques",
                    Slug = "react-performance-optimization".ToSlug(),
                    Content = @"React performance is crucial for user experience. This article covers essential optimization techniques:

1. **Code Splitting**
   - Dynamic imports
   - React.lazy and Suspense
   - Route-based splitting

2. **Memoization Strategies**
   - React.memo
   - useMemo hook
   - useCallback hook

3. **Component Optimization**
   - Avoiding unnecessary re-renders
   - Virtual lists for large datasets
   - Image lazy loading

4. **Bundle Analysis**
   - Using webpack-bundle-analyzer
   - Identifying bottlenecks
   - Tree shaking strategies

These techniques will significantly improve your React application's performance.",
                    Summary = "Essential techniques to optimize your React applications for better performance.",
                    Author = "Mostafa Said",
                    ImageUrl = "https://via.placeholder.com/400x300?text=React+Performance",
                    FeaturedImageUrl = "https://via.placeholder.com/800x400?text=React+Performance",
                    Status = BlogPostStatus.Published,
                    PublishedAt = DateTime.UtcNow.AddMonths(-1),
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
                    CreatedAt = DateTime.UtcNow.AddMonths(-1),
                    UpdatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new BlogPost
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    Title = "Docker and Kubernetes for Beginners",
                    Slug = "docker-kubernetes-beginners".ToSlug(),
                    Content = @"Containerization is essential in modern development. Let's explore Docker and Kubernetes:

1. **Understanding Docker**
   - What are containers?
   - Images vs Containers
   - Docker architecture

2. **Docker Basics**
   - Installing Docker
   - Running containers
   - Building images

3. **Kubernetes Essentials**
   - Pods and services
   - Deployments
   - ConfigMaps and Secrets

4. **Best Practices**
   - Image security
   - Resource management
   - Monitoring

Master these tools to deploy applications at scale.",
                    Summary = "A beginner-friendly introduction to Docker and Kubernetes for containerized applications.",
                    Author = "Mostafa Said",
                    ImageUrl = "https://via.placeholder.com/400x300?text=Docker+Kubernetes",
                    FeaturedImageUrl = "https://via.placeholder.com/800x400?text=Docker+Kubernetes",
                    Status = BlogPostStatus.Published,
                    PublishedAt = DateTime.UtcNow.AddDays(-10),
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
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow.AddDays(-8)
                }
            };

            modelBuilder.Entity<BlogPost>().HasData(blogPosts);
        }
    }
}
