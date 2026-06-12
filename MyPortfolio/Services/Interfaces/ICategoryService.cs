using MyPortfolio.Models.Entities;

namespace MyPortfolio.Services.Interfaces
{
    public interface ICategoryService
    {
        // Query operations
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetParentCategoriesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetChildCategoriesAsync(Guid parentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetCategoriesWithCountsAsync(CancellationToken cancellationToken = default);
        
        // Command operations
        Task<Category> CreateAsync(string name, string? description = null, Guid? parentId = null, CancellationToken cancellationToken = default);
        Task<Category> UpdateAsync(Guid id, string name, string? description = null, Guid? parentId = null, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        
        // Validation
        Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> HasChildrenAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
