using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models.ViewModels.Admin;

namespace MyPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recentProjects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();

            var recentBlogs = await _context.BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .ToListAsync();

            var recentMessages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .Take(5)
                .ToListAsync();

            var dashboardData = new DashboardViewModel
            {
                TotalProjects = await _context.Projects.CountAsync(),
                TotalBlogPosts = await _context.BlogPosts.CountAsync(),
                TotalMessages = await _context.ContactMessages.CountAsync(),
                RecentProjects = recentProjects,
                RecentBlogs = recentBlogs,
                RecentMessages = recentMessages
            };

            return View(dashboardData);
        }
    }
}
