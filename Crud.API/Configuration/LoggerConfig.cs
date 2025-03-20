using Crud.API.Extensions;

namespace Crud.API.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.AddHealthChecks()
                .AddCheck("Products", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));

            services.AddHealthChecksUI(opt =>
            {
                opt.AddHealthCheckEndpoint("Monitoring", "/api/hc");
            }).AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }
    }
}
