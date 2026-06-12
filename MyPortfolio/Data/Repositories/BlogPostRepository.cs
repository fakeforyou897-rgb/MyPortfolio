using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Repositories
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Include(b => b.BlogPostTags)
                .ThenInclude(bt => bt.Tag)
                .Where(b => b.IsFeatured && b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetPublishedPostsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Include(b => b.BlogPostTags)
                .ThenInclude(bt => bt.Tag)
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Include(b => b.BlogPostTags)
                .ThenInclude(bt => bt.Tag)
                .Where(b => b.CategoryId == categoryId && b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByTagAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Include(b => b.BlogPostTags)
                .ThenInclude(bt => bt.Tag)
                .Where(b => b.BlogPostTags.Any(bt => bt.TagId == tagId) && b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByStatusAsync(BlogPostStatus status, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Where(b => b.Status == status)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Include(b => b.BlogPostTags)
                .ThenInclude(bt => bt.Tag)
                .FirstOrDefaultAsync(b => b.Slug == slug, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> SearchPostsAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Where(b => b.IsPublished && (
                    b.Title.Contains(searchTerm) ||
                    b.Content.Contains(searchTerm) ||
                    (b.Summary != null && b.Summary.Contains(searchTerm))))
                .OrderByDescending(b => b.PublishedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task IncrementViewCountAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            var post = await GetByIdAsync(postId, cancellationToken);
            if (post != null)
            {
                post.ViewCount++;
                Update(post);
            }
        }
    }
}
