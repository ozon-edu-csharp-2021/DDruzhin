using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Events
{
    /// <summary>
    /// Пришла поставка с новыми товарами
    /// </summary>
    public class SupplyArrivedWithStockItemsDomainEvent : INotification
    {
        public Sku StockItemSku { get; }
        public int Quantity { get; }

        public SupplyArrivedWithStockItemsDomainEvent(Sku stockItemSku,
            int quantity)
        {
            StockItemSku = stockItemSku;
            Quantity = quantity;
        }
    }
}