using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Infrastructure.Data;
using Byakkoder.Product.Infrastructure.Data.Repositories;
using Byakkoder.Product.Infrastructure.ExternalDiscountApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Byakkoder.Product.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductManagementContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDiscountApiService, DiscountApiService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpClient();

            return services;
        }
    }
}
