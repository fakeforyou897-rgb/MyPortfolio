using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models.Entities;
using MyPortfolio.Extensions;

namespace MyPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.BlogPosts
                                      .OrderByDescending(b => b.CreatedAt)
                                      .ToListAsync();
            return View(blogs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.Id = Guid.NewGuid();
                blogPost.CreatedAt = DateTime.UtcNow;
                _context.BlogPosts.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null) return NotFound();
            return View(blogPost);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BlogPost blogPost)
        {
            if (id != blogPost.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.BlogPosts.Any(e => e.Id == blogPost.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null) return NotFound();
            return View(blogPost);
        }

        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
