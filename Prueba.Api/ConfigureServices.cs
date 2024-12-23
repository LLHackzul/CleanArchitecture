using System.Reflection;
using Microsoft.OpenApi.Models;
namespace Prueba.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prueba Backend",
                    Description = "Prueba Backend"
                });
            });
            return services;
        }
    }
}
