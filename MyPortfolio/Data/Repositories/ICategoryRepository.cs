using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetSubCategoriesAsync(Guid parentId, CancellationToken cancellationToken = default);
        Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);
    }
}
