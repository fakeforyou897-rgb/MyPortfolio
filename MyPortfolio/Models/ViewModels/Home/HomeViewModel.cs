using System.Collections.Generic;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Models.ViewModels.Home
{
    /// <summary>
    /// View model for the home page
    /// </summary>
    public class HomeViewModel
    {
        public List<Project> FeaturedProjects { get; set; } = new();
        
        public List<BlogPost> RecentBlogPosts { get; set; } = new();
        
        public int TotalProjects { get; set; }
        
        public int TotalBlogPosts { get; set; }
        
        public string? WelcomeMessage { get; set; }
        
        public string? AboutSummary { get; set; }
    }
}
