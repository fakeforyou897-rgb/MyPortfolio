using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data.UnitOfWork;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Common;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;
using MyPortfolio.Models.ViewModels;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly ITagService _tagService;

        public ProjectService(IUnitOfWork unitOfWork, IMappingService mappingService, ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _tagService = tagService;
        }

        // Query operations
        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.QueryIncluding(
                p => p.Category!,
                p => p.ProjectTags
            ).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetBySlugAsync(slug, cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetPublishedAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetPublishedProjectsAsync(cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetFeaturedAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetFeaturedProjectsAsync(count, cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetProjectsByCategoryAsync(categoryId, cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetByTagAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetProjectsByTagAsync(tagId, cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetByStatusAsync(ProjectStatus status, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.GetProjectsByStatusAsync(status, cancellationToken);
        }

        // Search and filter
        public async Task<PagedResult<Project>> SearchAsync(ProjectQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.Projects.Query();

            // Apply search
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.ApplySearch(parameters.SearchTerm, p => p.Title, p => p.Description, p => p.Technologies);
            }

            // Apply filters
            if (parameters.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == parameters.CategoryId.Value);
            }

            if (parameters.Status.HasValue)
            {
                query = query.Where(p => p.Status == parameters.Status.Value);
            }

            if (parameters.IsFeatured.HasValue)
            {
                query = query.Where(p => p.IsFeatured == parameters.IsFeatured.Value);
            }

            if (parameters.IsPublished.HasValue)
            {
                query = query.Where(p => p.IsPublished == parameters.IsPublished.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                query = query.ApplySort(parameters.SortBy, parameters.SortDescending);
            }
            else
            {
                query = query.OrderByDescending(p => p.CreatedAt);
            }

            // Apply pagination
            return await query.ToPagedResultAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
        }

        public async Task<IEnumerable<Project>> SearchByTermAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.SearchProjectsAsync(searchTerm, cancellationToken);
        }

        // Command operations
        public async Task<Project> CreateAsync(ProjectViewModel model, CancellationToken cancellationToken = default)
        {
            var project = _mappingService.MapToEntity(model);

            // Ensure unique slug
            var slug = project.Slug;
            var counter = 1;
            while (await SlugExistsAsync(slug, null, cancellationToken))
            {
                slug = $"{project.Slug}-{counter}";
                counter++;
            }
            project.Slug = slug;

            await _unitOfWork.Projects.AddAsync(project, cancellationToken);

            // Handle tags if provided
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                var tags = await _tagService.ParseTagsAsync(model.Tags, ',', cancellationToken);
                foreach (var tag in tags)
                {
                    project.ProjectTags.Add(new ProjectTag { ProjectId = project.Id, TagId = tag.Id });
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return project;
        }

        public async Task<Project> UpdateAsync(Guid id, ProjectViewModel model, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                throw new KeyNotFoundException($"Project with ID {id} not found");

            _mappingService.UpdateEntity(project, model);

            // Ensure unique slug (excluding current project)
            var slug = project.Slug;
            var counter = 1;
            while (await SlugExistsAsync(slug, id, cancellationToken))
            {
                slug = $"{model.Title.ToSlug()}-{counter}";
                counter++;
            }
            project.Slug = slug;

            _unitOfWork.Projects.Update(project);

            // Handle tags if provided
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                // Remove existing tags
                project.ProjectTags.Clear();

                // Add new tags
                var tags = await _tagService.ParseTagsAsync(model.Tags, ',', cancellationToken);
                foreach (var tag in tags)
                {
                    project.ProjectTags.Add(new ProjectTag { ProjectId = project.Id, TagId = tag.Id });
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return project;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            _unitOfWork.Projects.Remove(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            _unitOfWork.Projects.SoftDelete(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            _unitOfWork.Projects.Restore(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Business operations
        public async Task<bool> IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            project.ViewCount++;
            _unitOfWork.Projects.Update(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> ToggleFeaturedAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            project.IsFeatured = !project.IsFeatured;
            _unitOfWork.Projects.Update(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> TogglePublishAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            project.IsPublished = !project.IsPublished;
            _unitOfWork.Projects.Update(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> UpdateStatusAsync(Guid id, ProjectStatus status, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(id, cancellationToken);
            if (project == null)
                return false;

            project.Status = status;
            _unitOfWork.Projects.Update(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> AssignCategoryAsync(Guid projectId, Guid? categoryId, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(projectId, cancellationToken);
            if (project == null)
                return false;

            project.CategoryId = categoryId;
            _unitOfWork.Projects.Update(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> AssignTagsAsync(Guid projectId, List<Guid> tagIds, CancellationToken cancellationToken = default)
        {
            var project = await GetByIdAsync(projectId, cancellationToken);
            if (project == null)
                return false;

            // Remove existing tags
            project.ProjectTags.Clear();

            // Add new tags
            foreach (var tagId in tagIds)
            {
                project.ProjectTags.Add(new ProjectTag { ProjectId = projectId, TagId = tagId });
            }

            _unitOfWork.Projects.Update(project);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Validation
        public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.SlugExistsAsync(slug, excludeId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Projects.AnyAsync(p => p.Id == id, cancellationToken);
        }
    }
}
