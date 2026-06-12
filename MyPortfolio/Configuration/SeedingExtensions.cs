using Microsoft.AspNetCore.Identity;
using MyPortfolio.Data;

namespace MyPortfolio.Configuration
{
    /// <summary>
    /// Extension methods for database seeding
    /// </summary>
    public static class SeedingExtensions
    {
        /// <summary>
        /// Seed the database with initial data at application startup
        /// </summary>
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedAdminUserAsync(services);
            }
        }

        /// <summary>
        /// Seed default admin user if it doesn't exist
        /// </summary>
        private static async Task SeedAdminUserAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            const string adminRole = "Admin";
            const string adminEmail = "m.ssaid356@gmail.com";
            const string adminPassword = "Memo@356000";

            // Create Admin Role if it doesn't exist
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Create Admin User if it doesn't exist
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Assign admin role to the user
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
                else
                {
                    // Log errors if user creation fails
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating admin user: {error.Description}");
                    }
                }
            }
        }
    }
}
