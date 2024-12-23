using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prueba.Application.Common.Interfaces;
using Prueba.Application.Common.Interfaces.OrderInterface.Write;
using Prueba.Application.Common.Interfaces.ProductInterface.Read;
using Prueba.Application.Common.Interfaces.ProductInterface.Write;
using Prueba.Infrastructure.DataContext;
using Prueba.Infrastructure.Repositories.OrderRepository;
using Prueba.Infrastructure.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructure(
          this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddDbContext<DatabaseTestContext>(
                options =>
                options.UseMySql(
                   configuration.GetConnectionString("prueba"),
                   new MySqlServerVersion(new Version(8, 0, 29))
                   )
                );
            services.AddScoped<IOrderWriteRepository, OrderRepository>();

            services.AddScoped<IProductReadRepository, ProductRepository>();

            services.AddScoped<IProductWriteRepository, ProductRepository>();
            return services;
        }
    }
}
