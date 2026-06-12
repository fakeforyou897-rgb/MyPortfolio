using System.Linq.Expressions;
using MyPortfolio.Models.Common;

namespace MyPortfolio.Data.Repositories
{
    /// <summary>
    /// Generic repository interface for common CRUD operations
    /// </summary>
    public interface IRepository<T> where T : BaseEntity
    {
        // Query operations
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

        // Query with includes
        IQueryable<T> Query();
        IQueryable<T> QueryIncluding(params Expression<Func<T, object>>[] includes);

        // Pagination
        Task<PagedResult<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            CancellationToken cancellationToken = default);

        // Command operations
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        // Soft delete
        void SoftDelete(T entity);
        void SoftDeleteRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetDeletedAsync(CancellationToken cancellationToken = default);
        void Restore(T entity);
    }
}
