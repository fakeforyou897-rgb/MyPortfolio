using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.DisplayOrder)
                .ThenByDescending(p => p.CreatedAt)
                .ToListAsync();
                
            return View(projects);
        }

        // GET: Projects/Details/{slug}
        public async Task<IActionResult> Details(string? slug)
        {
            if (slug == null) return NotFound();

            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.ProjectTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(m => m.Slug == slug && m.IsPublished);

            if (project == null) return NotFound();

            // Increment view count
            project.ViewCount++;
            await _context.SaveChangesAsync();

            return View(project);
        }
    }
}
