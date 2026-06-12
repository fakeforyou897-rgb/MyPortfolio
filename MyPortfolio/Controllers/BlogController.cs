using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Blog
        public async Task<IActionResult> Index()
        {
            var posts = await _context.BlogPosts
                .Include(p => p.Category)
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
            return View(posts);
        }

        // GET: /Blog/Details/{slug}
        public async Task<IActionResult> Details(string? slug)
        {
            if (slug == null) return NotFound();

            var post = await _context.BlogPosts
                .Include(p => p.Category)
                .Include(p => p.BlogPostTags)
                .ThenInclude(bt => bt.Tag)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);
                
            if (post == null) return NotFound();

            // Increment view count
            post.ViewCount++;
            await _context.SaveChangesAsync();

            return View(post);
        }
    }
}
