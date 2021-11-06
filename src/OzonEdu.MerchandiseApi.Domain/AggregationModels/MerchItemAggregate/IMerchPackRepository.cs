using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.Contracts;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate
{
    public interface IMerchItemRepository : IRepository<MerchItem>
    {
        Task<MerchItem> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<MerchItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
        Task<IEnumerable<long>> CreateMerchItemsAsync(IEnumerable<long> merchItems, CancellationToken cancellationToken = default);
    }
}