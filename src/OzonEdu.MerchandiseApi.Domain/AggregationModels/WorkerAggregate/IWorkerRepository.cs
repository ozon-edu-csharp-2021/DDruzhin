using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.Contracts;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        Task<Worker> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Worker> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<Worker> CreateAsync(Worker worker, CancellationToken cancellationToken = default);
    }
}