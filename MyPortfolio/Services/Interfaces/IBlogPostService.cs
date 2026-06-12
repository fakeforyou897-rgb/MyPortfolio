using MyPortfolio.Models.Common;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;
using MyPortfolio.Models.ViewModels;

namespace MyPortfolio.Services.Interfaces
{
    public interface IBlogPostService
    {
        // Query operations
        Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetPublishedAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetByTagAsync(Guid tagId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetByStatusAsync(BlogPostStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetByAuthorAsync(string author, CancellationToken cancellationToken = default);
        
        // Search and filter
        Task<PagedResult<BlogPost>> SearchAsync(BlogPostQueryParameters parameters, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> SearchByTermAsync(string searchTerm, CancellationToken cancellationToken = default);
        
        // Command operations
        Task<BlogPost> CreateAsync(BlogPostViewModel model, CancellationToken cancellationToken = default);
        Task<BlogPost> UpdateAsync(Guid id, BlogPostViewModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        
        // Business operations
        Task<bool> IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ToggleFeaturedAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> PublishAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UnpublishAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(Guid id, BlogPostStatus status, CancellationToken cancellationToken = default);
        Task<bool> AssignCategoryAsync(Guid postId, Guid? categoryId, CancellationToken cancellationToken = default);
        Task<bool> AssignTagsAsync(Guid postId, List<Guid> tagIds, CancellationToken cancellationToken = default);
        Task<int> CalculateReadingTimeAsync(string content, CancellationToken cancellationToken = default);
        
        // Validation
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
