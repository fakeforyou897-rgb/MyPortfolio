using MyPortfolio.Data.Repositories;
using MyPortfolio.Data.UnitOfWork;

namespace MyPortfolio.Configuration.Repositories
{
    /// <summary>
    /// Extension methods for registering repositories and unit of work
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Register Unit of Work and all repositories
        /// </summary>
        public static IServiceCollection AddRepositoriesConfiguration(this IServiceCollection services)
        {
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();

            return services;
        }
    }
}
