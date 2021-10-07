using Catalog.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Catalog.Extensions
{
    public static class MongoDbServiceExtensions
    {
        public static IServiceCollection AddMongoClient(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IMongoClient>(serviceProvider => {
                var settings = config.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            return services;
        }
    }
}