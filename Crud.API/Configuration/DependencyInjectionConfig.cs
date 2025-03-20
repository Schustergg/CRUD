using Crud.API.Extensions;
using Crud.Business.Interfaces;
using Crud.Business.Notifications;
using Crud.Business.Services;
using Crud.Data.Context;
using Crud.Data.Repository;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Crud.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<CrudDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();


            return services;
        }
    }
}
