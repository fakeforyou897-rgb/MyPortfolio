using System;
using System.Collections.Generic;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Models.ViewModels.Admin
{
    /// <summary>
    /// View model for the admin dashboard
    /// </summary>
    public class DashboardViewModel
    {
        public int TotalProjects { get; set; }
        
        public int TotalBlogPosts { get; set; }
        
        public int TotalMessages { get; set; }
        
        public int UnreadMessages { get; set; }
        
        public int PublishedPosts { get; set; }
        
        public int DraftPosts { get; set; }
        
        public int ActiveProjects { get; set; }
        
        public int CompletedProjects { get; set; }
        
        public required List<Project> RecentProjects { get; set; }
        
        public required List<BlogPost> RecentBlogs { get; set; }
        
        public required List<ContactMessage> RecentMessages { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Statistics summary for admin dashboard
    /// </summary>
    public class DashboardStatistics
    {
        public int TotalVisitors { get; set; }
        
        public int TotalPageViews { get; set; }
        
        public int BlogViewsThisMonth { get; set; }
        
        public int MessagesThisMonth { get; set; }
        
        public double AverageResponseTime { get; set; }
        
        public List<string> PopularTags { get; set; } = new();
        
        public List<string> TopProjects { get; set; } = new();
    }
}
