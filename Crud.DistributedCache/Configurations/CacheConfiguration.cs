using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Cache.Configurations
{
    public static class CacheConfiguration
    {
        public static void ConfigureCacheService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationPrimary = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);

                var endpoints = new EndPointCollection();

                foreach (var endpoint in configurationPrimary.EndPoints)
                {
                    endpoints.Add(endpoint);
                }

                var connection = ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = endpoints,
                    Ssl = false,
                    AbortOnConnectFail = false
                });

                return connection;

            });

            services.AddScoped<IDistributedCacheService, DistributedCacheService>();
        }
    }
}
