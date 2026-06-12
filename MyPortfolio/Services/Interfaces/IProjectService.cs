using MyPortfolio.Models.Common;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;
using MyPortfolio.Models.ViewModels;

namespace MyPortfolio.Services.Interfaces
{
    public interface IProjectService
    {
        // Query operations
        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetPublishedAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetFeaturedAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetByTagAsync(Guid tagId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetByStatusAsync(ProjectStatus status, CancellationToken cancellationToken = default);
        
        // Search and filter
        Task<PagedResult<Project>> SearchAsync(ProjectQueryParameters parameters, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> SearchByTermAsync(string searchTerm, CancellationToken cancellationToken = default);
        
        // Command operations
        Task<Project> CreateAsync(ProjectViewModel model, CancellationToken cancellationToken = default);
        Task<Project> UpdateAsync(Guid id, ProjectViewModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        
        // Business operations
        Task<bool> IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ToggleFeaturedAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> TogglePublishAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(Guid id, ProjectStatus status, CancellationToken cancellationToken = default);
        Task<bool> AssignCategoryAsync(Guid projectId, Guid? categoryId, CancellationToken cancellationToken = default);
        Task<bool> AssignTagsAsync(Guid projectId, List<Guid> tagIds, CancellationToken cancellationToken = default);
        
        // Validation
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
