using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.Contracts;

namespace OzonEdu.MerchandiseApi.Infrastructure.Stubs
{
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

}