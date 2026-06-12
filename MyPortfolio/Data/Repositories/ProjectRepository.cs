using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetFeaturedProjectsAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.ProjectTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.IsFeatured && p.IsPublished)
                .OrderByDescending(p => p.DisplayOrder)
                .ThenByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetPublishedProjectsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.ProjectTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.DisplayOrder)
                .ThenByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetProjectsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.ProjectTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.CategoryId == categoryId && p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetProjectsByTagAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.ProjectTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.ProjectTags.Any(pt => pt.TagId == tagId) && p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.Status == status)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.ProjectTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken);
        }

        public async Task<IEnumerable<Project>> SearchProjectsAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.IsPublished && (
                    p.Title.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm) ||
                    p.Technologies.Contains(searchTerm)))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task IncrementViewCountAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(projectId, cancellationToken);
            if (project != null)
            {
                project.ViewCount++;
                Update(project);
            }
        }
    }
}
