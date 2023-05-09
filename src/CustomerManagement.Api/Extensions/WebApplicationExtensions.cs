using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Api.Extensions
{
    /// <summary>
    ///     Helper extensions for WebApplication
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        ///     Migrate database of specified context type
        /// </summary>
        /// <param name="webApplication"></param>
        /// <typeparam name="T"></typeparam>
        public static void MigrateDatabase<T>(this WebApplication webApplication) where T : DbContext
        {
            using var scope = webApplication.Services.CreateScope();

            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database");
            }
        }
    }
}