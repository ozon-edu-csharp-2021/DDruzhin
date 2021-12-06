using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using CSharpCourse.Core.Lib.Events;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OzonEdu.MerchandiseApi.Domain.Contracts;
using OzonEdu.MerchandiseApi.GrpcServices;
using OzonEdu.MerchandiseApi.HostedServices;
using OzonEdu.MerchandiseApi.Infrastructure.Configuration;
using OzonEdu.MerchandiseApi.Infrastructure.Extensions;
using OzonEdu.MerchandiseApi.Infrastructure.Interceptors;
using OzonEdu.MerchandiseApi.Infrastructure.KafkaProducers;
using OzonEdu.MerchandiseApi.Infrastructure.Repositories.Infrastructure;
using OzonEdu.MerchandiseApi.Infrastructure.Repositories.Infrastructure.Interfaces;
using Tracer = Jaeger.Tracer;

namespace OzonEdu.MerchandiseApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<MerchPackRequestHostedServices>();
            services.AddHostedService<NewSupplyHostedServices>();

            services.AddInfrastructureServices();
            services.AddInfrastructureRepositories();

            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

            services.AddScoped<IEmailNotificationProducer, EmailNotificationProducer>();
            services.AddSingleton<IProducer<long, NotificationEvent>>(provider =>
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092"
                };
                var builder = new ProducerBuilder<long, NotificationEvent>(config);
                builder.SetValueSerializer(new SerializerMerchPack<NotificationEvent>());
                return builder.Build();
            });

            services.AddSingleton<IConsumer<long, NotificationEvent>>(provider =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "MerchPackConsumer",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };
                var builder = new ConsumerBuilder<long, NotificationEvent>(config);
                builder.SetValueDeserializer(new SerializerMerchPack<NotificationEvent>());
                return builder.Build();
            });

            services.AddSingleton<IConsumer<long, StockReplenishedEvent>>(provider =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "StockReplenishedConsumer",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };
                var builder = new ConsumerBuilder<long, CSharpCourse.Core.Lib.Events.StockReplenishedEvent>(config);
                builder.SetValueDeserializer(
                    new SerializerMerchPack<CSharpCourse.Core.Lib.Events.StockReplenishedEvent>());
                return builder.Build();
            });

            services.AddSingleton<ITracer>(
                sp =>
                {
                    var loggerFactory = sp.GetService<ILoggerFactory>();
                    
                    Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
                        .RegisterSenderFactory<ThriftSenderFactory>();
                    
                    var sampler = new ConstSampler(true);
                    
                    var tracer = new Tracer.Builder("MerchandiseApi")
                        .WithLoggerFactory(loggerFactory)
                        .WithSampler(sampler);

                    var trace = tracer.Build();
                    
                    return trace;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchandiseApiGrpService>();
                endpoints.MapControllers();
            });
        }
    }
}