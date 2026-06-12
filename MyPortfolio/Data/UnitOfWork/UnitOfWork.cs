using Microsoft.EntityFrameworkCore.Storage;
using MyPortfolio.Data.Repositories;

namespace MyPortfolio.Data.UnitOfWork
{
    /// <summary>
    /// Unit of Work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        // Repository instances
        private IProjectRepository? _projects;
        private IBlogPostRepository? _blogPosts;
        private ICategoryRepository? _categories;
        private ITagRepository? _tags;
        private IContactMessageRepository? _contactMessages;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lazy-loaded repositories
        public IProjectRepository Projects => 
            _projects ??= new ProjectRepository(_context);

        public IBlogPostRepository BlogPosts => 
            _blogPosts ??= new BlogPostRepository(_context);

        public ICategoryRepository Categories => 
            _categories ??= new CategoryRepository(_context);

        public ITagRepository Tags => 
            _tags ??= new TagRepository(_context);

        public IContactMessageRepository ContactMessages => 
            _contactMessages ??= new ContactMessageRepository(_context);

        // Save changes
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesReturnBoolAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        // Transaction management
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                
                if (_transaction != null)
                {
                    await _transaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // Dispose
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
