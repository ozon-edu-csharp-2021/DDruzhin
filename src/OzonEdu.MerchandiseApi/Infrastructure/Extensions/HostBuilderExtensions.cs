using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Filters;
using OzonEdu.MerchandiseApi.Infrastructure.Interceptors;
using OzonEdu.MerchandiseApi.Infrastructure.StartupFilters;

namespace OzonEdu.MerchandiseApi.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                
                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "OzonEdu.MerchandiseApi", Version = "v1"});
                
                    options.CustomSchemaIds(x => x.FullName);
                });
            });
            return builder;
        }
        
        public static IHostBuilder AddHttp(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
            });
            
            return builder;
        }
    }
}