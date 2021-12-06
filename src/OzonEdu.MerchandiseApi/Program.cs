using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using OzonEdu.MerchandiseApi.Infrastructure.Extensions;
using Serilog;

namespace OzonEdu.MerchandiseApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog(
                    (context, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ConfigureEndpointDefaults(lo =>
                            lo.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                })
                .AddInfrastructure()
                .AddHttp();
        }
    }
}