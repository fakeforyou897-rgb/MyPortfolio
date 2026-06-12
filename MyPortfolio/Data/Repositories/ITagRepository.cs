using MyPortfolio.Models.Entities;

namespace MyPortfolio.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetActiveTagsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tag>> GetPopularTagsAsync(int count, CancellationToken cancellationToken = default);
        Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task IncrementUsageCountAsync(Guid tagId, CancellationToken cancellationToken = default);
        Task DecrementUsageCountAsync(Guid tagId, CancellationToken cancellationToken = default);
    }
}
