using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetPublishedPostsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetPostsByTagAsync(Guid tagId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetPostsByStatusAsync(BlogPostStatus status, CancellationToken cancellationToken = default);
        Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> SearchPostsAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int count, CancellationToken cancellationToken = default);
        Task IncrementViewCountAsync(Guid postId, CancellationToken cancellationToken = default);
    }
}
