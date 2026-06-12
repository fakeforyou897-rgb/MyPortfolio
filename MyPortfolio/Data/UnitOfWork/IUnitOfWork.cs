using MyPortfolio.Data.Repositories;

namespace MyPortfolio.Data.UnitOfWork
{
    /// <summary>
    /// Unit of Work pattern to manage transactions and repository access
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Repositories
        IProjectRepository Projects { get; }
        IBlogPostRepository BlogPosts { get; }
        ICategoryRepository Categories { get; }
        ITagRepository Tags { get; }
        IContactMessageRepository ContactMessages { get; }

        // Transaction methods
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveChangesReturnBoolAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync();
    }
}
