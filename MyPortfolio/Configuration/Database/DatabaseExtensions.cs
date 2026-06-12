using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;

namespace MyPortfolio.Configuration.Database
{
    /// <summary>
    /// Extension methods for database configuration
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Configure SQLite database context
        /// </summary>
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
