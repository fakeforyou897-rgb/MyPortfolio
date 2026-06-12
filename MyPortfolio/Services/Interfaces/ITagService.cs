using MyPortfolio.Models.Entities;

namespace MyPortfolio.Services.Interfaces
{
    public interface ITagService
    {
        // Query operations
        Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetPopularAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetTagsWithCountsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetByBlogPostAsync(Guid blogPostId, CancellationToken cancellationToken = default);
        
        // Command operations
        Task<Tag> CreateAsync(string name, CancellationToken cancellationToken = default);
        Task<Tag> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        
        // Tag management
        Task<IEnumerable<Tag>> GetOrCreateTagsAsync(IEnumerable<string> tagNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> ParseTagsAsync(string tagString, char separator = ',', CancellationToken cancellationToken = default);
        
        // Validation
        Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
