using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.Contracts;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository : IRepository<MerchPack>
    {
        Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<MerchPack>> FindByWorkerEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<MerchPack> CreateMerchPackAsync(MerchPack merchPack, CancellationToken cancellationToken = default);
    }
}