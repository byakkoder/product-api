using Byakkoder.Product.Api.Handlers;
using Byakkoder.Product.Api.Interfaces;
using System.Reflection;

namespace Byakkoder.Product.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IProductHandler, ProductHandler>();

            return services;
        }
    }
}
