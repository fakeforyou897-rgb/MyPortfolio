using Microsoft.AspNetCore.Identity;
using MyPortfolio.Data;

namespace MyPortfolio.Configuration.Identity
{
    /// <summary>
    /// Extension methods for ASP.NET Core Identity configuration
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Configure ASP.NET Core Identity with custom options
        /// </summary>
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
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

            return services;
        }
    }
}
