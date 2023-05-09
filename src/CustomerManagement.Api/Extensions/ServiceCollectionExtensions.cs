namespace CustomerManagement.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwaggerAndConfig(this IServiceCollection serviceCollection)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();
        }

        public static void AddHealthChecks(this IServiceCollection serviceCollection)
        {
            
        }
    }
}