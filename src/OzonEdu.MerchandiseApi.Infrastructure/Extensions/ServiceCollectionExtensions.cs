using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.Contracts;
using OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate;

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
            services.AddScoped<IMerchPackRepository,MerchPackRepository>();
            services.AddScoped<IWorkerRepository,WorkerRepository>();
            
            return services;
        }
    }
    public class WorkerRepository : IWorkerRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public Task<Worker> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Worker> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        Task<Worker> IWorkerRepository.CreateAsync(Worker worker, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        Task<Worker> IRepository<Worker>.CreateAsync(Worker itemToCreate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Worker> UpdateAsync(Worker itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MerchPackRepository : IMerchPackRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MerchPack>> FindByWorkerAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPack> CreateMerchPackAsync(MerchPack merchPack, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}