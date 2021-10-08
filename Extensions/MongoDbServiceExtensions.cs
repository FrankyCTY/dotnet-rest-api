using Catalog.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Catalog.Extensions
{
    public static class MongoDbServiceExtensions
    {
        public static IServiceCollection AddMongoClient(this IServiceCollection services, IConfiguration config, MongoDbSettings settings)
        {
            services.AddSingleton<IMongoClient>(serviceProvider => {
                return new MongoClient(settings.ConnectionString);
            });

            return services;
        }
    }
}