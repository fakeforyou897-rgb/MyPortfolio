using MyPortfolio.Services;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Configuration.Services
{
    /// <summary>
    /// Extension methods for registering business logic services
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Register all business logic services
        /// </summary>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Mapping Service
            services.AddScoped<IMappingService, MappingService>();

            // Domain Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IContactMessageService, ContactMessageService>();

            return services;
        }
    }
}
