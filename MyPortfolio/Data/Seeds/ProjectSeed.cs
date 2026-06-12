using Microsoft.EntityFrameworkCore;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Seeds
{
    /// <summary>
    /// Seeder for sample Project data
    /// </summary>
    public class ProjectSeed : ISeeder
    {
        public int Order => 3; // Execute after categories and tags

        public void Seed(ModelBuilder modelBuilder)
        {
            var projects = new List<Project>
            {
                new Project
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Title = "E-Commerce Platform",
                    Slug = "e-commerce-platform".ToSlug(),
                    Description = "A full-featured e-commerce platform built with ASP.NET Core and React. Includes user authentication, product catalog, shopping cart, payment integration, and admin dashboard.",
                    ShortDescription = "Full-featured e-commerce platform with ASP.NET Core and React",
                    Technologies = "ASP.NET Core,React,SQL Server,Docker",
                    ImageUrl = "https://via.placeholder.com/400x300?text=E-Commerce",
                    ThumbnailUrl = "https://via.placeholder.com/200x150?text=E-Commerce",
                    GitHubUrl = "https://github.com/mostafa-said/ecommerce",
                    LiveDemoUrl = "https://ecommerce-demo.example.com",
                    Status = ProjectStatus.Completed,
                    Priority = Priority.High,
                    StartDate = DateTime.UtcNow.AddMonths(-12),
                    EndDate = DateTime.UtcNow.AddMonths(-3),
                    DisplayOrder = 1,
                    IsFeatured = true,
                    IsPublished = true,
                    Rating = 5,
                    ViewCount = 245,
                    LikeCount = 42,
                    ClientName = "TechStart Inc",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow.AddMonths(-12),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-3)
                },
                new Project
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Title = "Real-Time Chat Application",
                    Slug = "realtime-chat-application".ToSlug(),
                    Description = "Real-time chat application with WebSocket support, user presence, file sharing, and message history. Built with ASP.NET Core SignalR and Angular.",
                    ShortDescription = "Real-time chat with SignalR and Angular",
                    Technologies = "ASP.NET Core,SignalR,Angular,PostgreSQL",
                    ImageUrl = "https://via.placeholder.com/400x300?text=Chat+App",
                    ThumbnailUrl = "https://via.placeholder.com/200x150?text=Chat+App",
                    GitHubUrl = "https://github.com/mostafa-said/chatapp",
                    LiveDemoUrl = "https://chat-demo.example.com",
                    Status = ProjectStatus.Completed,
                    Priority = Priority.High,
                    StartDate = DateTime.UtcNow.AddMonths(-9),
                    EndDate = DateTime.UtcNow.AddMonths(-2),
                    DisplayOrder = 2,
                    IsFeatured = true,
                    IsPublished = true,
                    Rating = 5,
                    ViewCount = 312,
                    LikeCount = 58,
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow.AddMonths(-9),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-2)
                },
                new Project
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Title = "Task Management System",
                    Slug = "task-management-system".ToSlug(),
                    Description = "Collaborative task management system with team workspaces, kanban boards, and real-time updates. Built with Node.js, React, and MongoDB.",
                    ShortDescription = "Collaborative task management with kanban boards",
                    Technologies = "Node.js,React,MongoDB,Docker",
                    ImageUrl = "https://via.placeholder.com/400x300?text=Task+Manager",
                    ThumbnailUrl = "https://via.placeholder.com/200x150?text=Task+Manager",
                    GitHubUrl = "https://github.com/mostafa-said/taskmanager",
                    Status = ProjectStatus.InProgress,
                    Priority = Priority.High,
                    StartDate = DateTime.UtcNow.AddMonths(-6),
                    DisplayOrder = 3,
                    IsFeatured = false,
                    IsPublished = true,
                    ViewCount = 178,
                    LikeCount = 31,
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow.AddMonths(-6)
                }
            };

            modelBuilder.Entity<Project>().HasData(projects);
        }
    }
}
