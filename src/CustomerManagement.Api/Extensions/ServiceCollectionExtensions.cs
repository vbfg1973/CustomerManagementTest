namespace CustomerManagement.Api.Extensions
{
    /// <summary>
    ///     Helper extensions for populating Service Collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add swagger support and configure appropriately
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddSwaggerAndConfig(this IServiceCollection serviceCollection)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
        }

        /// <summary>
        ///     Add any health checks required
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddHealthChecks(this IServiceCollection serviceCollection)
        {
        }
    }
}