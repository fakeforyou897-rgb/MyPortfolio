using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Seeds
{
    public class ProjectSeed : ISeeder
    {
        public int Order => 3;

        public void Seed(ModelBuilder modelBuilder)
        {
            var now = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var projects = new List<Project>
            {
                new Project
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Title = "E-Commerce Platform",
                    Slug = "e-commerce-platform",
                    Description = "A full-featured e-commerce platform built with ASP.NET Core and React. Includes user authentication, product catalog, shopping cart, payment integration, and admin dashboard.",
                    ShortDescription = "Full-featured e-commerce platform with ASP.NET Core and React",
                    Technologies = "ASP.NET Core,React,SQL Server,Docker",
                    ImageUrl = "https://via.placeholder.com/400x300?text=E-Commerce",
                    ThumbnailUrl = "https://via.placeholder.com/200x150?text=E-Commerce",
                    GitHubUrl = "https://github.com/mostafa-said/ecommerce",
                    LiveDemoUrl = "https://ecommerce-demo.example.com",
                    Status = ProjectStatus.Completed,
                    Priority = Priority.High,
                    StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                    DisplayOrder = 1,
                    IsFeatured = true,
                    IsPublished = true,
                    Rating = 5,
                    ViewCount = 245,
                    LikeCount = 42,
                    ClientName = "TechStart Inc",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Title = "Real-Time Chat Application",
                    Slug = "realtime-chat-application",
                    Description = "Real-time chat application with WebSocket support, user presence, file sharing, and message history. Built with ASP.NET Core SignalR and Angular.",
                    ShortDescription = "Real-time chat with SignalR and Angular",
                    Technologies = "ASP.NET Core,SignalR,Angular,PostgreSQL",
                    ImageUrl = "https://via.placeholder.com/400x300?text=Chat+App",
                    ThumbnailUrl = "https://via.placeholder.com/200x150?text=Chat+App",
                    GitHubUrl = "https://github.com/mostafa-said/chatapp",
                    LiveDemoUrl = "https://chat-demo.example.com",
                    Status = ProjectStatus.Completed,
                    Priority = Priority.High,
                    StartDate = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                    DisplayOrder = 2,
                    IsFeatured = true,
                    IsPublished = true,
                    Rating = 5,
                    ViewCount = 312,
                    LikeCount = 58,
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Project
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Title = "Task Management System",
                    Slug = "task-management-system",
                    Description = "Collaborative task management system with team workspaces, kanban boards, and real-time updates. Built with Node.js, React, and MongoDB.",
                    ShortDescription = "Collaborative task management with kanban boards",
                    Technologies = "Node.js,React,MongoDB,Docker",
                    ImageUrl = "https://via.placeholder.com/400x300?text=Task+Manager",
                    ThumbnailUrl = "https://via.placeholder.com/200x150?text=Task+Manager",
                    GitHubUrl = "https://github.com/mostafa-said/taskmanager",
                    Status = ProjectStatus.InProgress,
                    Priority = Priority.High,
                    StartDate = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    DisplayOrder = 3,
                    IsFeatured = false,
                    IsPublished = true,
                    ViewCount = 178,
                    LikeCount = 31,
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            modelBuilder.Entity<Project>().HasData(projects);
        }
    }
}
