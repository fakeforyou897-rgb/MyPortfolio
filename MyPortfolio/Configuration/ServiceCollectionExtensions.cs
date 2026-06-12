using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Data.Repositories;
using MyPortfolio.Data.UnitOfWork;
using MyPortfolio.Services;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Configuration
{
    /// <summary>
    /// Extension methods for registering services in the dependency injection container
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all application services to the service collection
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Database
            AddDatabase(services, configuration);

            // Add Repositories
            AddRepositories(services);

            // Add Services
            AddBusinessServices(services);

            // Add Identity
            AddIdentity(services);

            // Add MVC
            AddMvcServices(services);

            return services;
        }

        /// <summary>
        /// Configure SQLite database context
        /// </summary>
        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// Register Unit of Work and all repositories
        /// </summary>
        private static void AddRepositories(IServiceCollection services)
        {
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
        }

        /// <summary>
        /// Register business logic services
        /// </summary>
        private static void AddBusinessServices(IServiceCollection services)
        {
            // Mapping Service
            services.AddScoped<IMappingService, MappingService>();

            // Domain Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IContactMessageService, ContactMessageService>();
        }

        /// <summary>
        /// Configure ASP.NET Core Identity
        /// </summary>
        private static void AddIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        /// <summary>
        /// Register MVC and view services
        /// </summary>
        private static void AddMvcServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddApplicationInsightsTelemetry();
            services.AddRazorPages();
        }
    }
}
