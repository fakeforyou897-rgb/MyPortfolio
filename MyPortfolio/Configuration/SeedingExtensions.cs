using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;

namespace MyPortfolio.Configuration
{
    public static class SeedingExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Create the database schema from the current model
                var db = services.GetRequiredService<ApplicationDbContext>();
                await db.Database.EnsureCreatedAsync();

                await SeedAdminUserAsync(services);
            }
        }

        private static async Task SeedAdminUserAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            const string adminRole = "Admin";
            const string adminEmail = "m.ssaid356@gmail.com";
            const string adminPassword = "Memo@356000";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

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
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating admin user: {error.Description}");
                    }
                }
            }
        }
    }
}
