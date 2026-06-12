namespace MyPortfolio.Configuration.Mvc
{
    /// <summary>
    /// Extension methods for MVC and view services configuration
    /// </summary>
    public static class MvcExtensions
    {
        /// <summary>
        /// Register MVC, Razor Pages, and view services
        /// </summary>
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddApplicationInsightsTelemetry();
            services.AddRazorPages();

            return services;
        }
    }
}
