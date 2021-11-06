using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate;
using OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Infrastructure.Stubs;

namespace OzonEdu.MerchandiseApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MerchPackRequestCommandHandler).Assembly);

            return services;
        }

        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IMerchItemRepository, MerchItemRepository>();

            return services;
        }
    }
    
}