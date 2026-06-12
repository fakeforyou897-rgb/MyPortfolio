namespace MyPortfolio.Configuration
{
    /// <summary>
    /// Extension methods for configuring the application middleware pipeline
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configure HTTP middleware pipeline
        /// </summary>
        public static WebApplication UseApplicationMiddleware(this WebApplication app)
        {
            // Error handling and HTTPS
            ConfigureErrorHandling(app);

            // Static files and routing
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseRouting();

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        /// <summary>
        /// Configure routing and endpoints
        /// </summary>
        public static WebApplication MapApplicationEndpoints(this WebApplication app)
        {
            // Area Routes (must come first)
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=AdminDashboard}/{action=Index}/{id?}");

            // Default MVC Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Identity Razor Pages
            app.MapRazorPages();

            return app;
        }

        /// <summary>
        /// Configure error handling middleware
        /// </summary>
        private static void ConfigureErrorHandling(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
        }
    }
}
