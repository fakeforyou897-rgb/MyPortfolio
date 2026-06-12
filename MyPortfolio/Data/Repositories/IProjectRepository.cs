using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Data.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetFeaturedProjectsAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetPublishedProjectsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetProjectsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetProjectsByTagAsync(Guid tagId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status, CancellationToken cancellationToken = default);
        Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> SearchProjectsAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task IncrementViewCountAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
