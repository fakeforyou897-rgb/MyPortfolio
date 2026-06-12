using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyPortfolio.Data;

namespace MyPortfolio.Configuration.Database
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                       .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

            return services;
        }
    }
}
