using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tag>> GetActiveTagsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(t => t.IsActive)
                .OrderBy(t => t.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Tag>> GetPopularTagsAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(t => t.IsActive)
                .OrderByDescending(t => t.UsageCount)
                .ThenBy(t => t.Name)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Slug == slug, cancellationToken);
        }

        public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(t => t.Slug == slug);
            
            if (excludeId.HasValue)
                query = query.Where(t => t.Id != excludeId.Value);
            
            return await query.AnyAsync(cancellationToken);
        }

        public async Task IncrementUsageCountAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            var tag = await GetByIdAsync(tagId, cancellationToken);
            if (tag != null)
            {
                tag.UsageCount++;
                Update(tag);
            }
        }

        public async Task DecrementUsageCountAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            var tag = await GetByIdAsync(tagId, cancellationToken);
            if (tag != null && tag.UsageCount > 0)
            {
                tag.UsageCount--;
                Update(tag);
            }
        }
    }
}
