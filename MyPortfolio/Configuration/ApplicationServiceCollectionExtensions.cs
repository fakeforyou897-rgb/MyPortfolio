using MyPortfolio.Configuration.Database;
using MyPortfolio.Configuration.Identity;
using MyPortfolio.Configuration.Mvc;
using MyPortfolio.Configuration.Repositories;
using MyPortfolio.Configuration.Services;

namespace MyPortfolio.Configuration
{
    /// <summary>
    /// Main orchestrator for all service collection extensions
    /// Coordinates all configuration modules in the correct order
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Add all application services to the service collection
        /// Registers all dependencies: Database, Repositories, Services, Identity, and MVC
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Database Configuration
            services.AddDatabaseConfiguration(configuration);

            // 2. Repository Pattern & Unit of Work
            services.AddRepositoriesConfiguration();

            // 3. Business Logic Services
            services.AddBusinessServices();

            // 4. Identity Management
            services.AddIdentityConfiguration();

            // 5. MVC & View Services
            services.AddMvcConfiguration();

            return services;
        }
    }
}
