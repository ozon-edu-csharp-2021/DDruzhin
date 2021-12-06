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
        Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken);
        Task<MerchItem> FindByIdAsync(long id, CancellationToken cancellationToken);
        Task<MerchItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken);
        Task<MerchItem> CreateMerchItemAsync(MerchItem merchItem, CancellationToken cancellationToken);
        Task<IEnumerable<MerchItem>> CreateMerchItemsAsync(IEnumerable<MerchItem> merchItems, CancellationToken cancellationToken);
    }
}