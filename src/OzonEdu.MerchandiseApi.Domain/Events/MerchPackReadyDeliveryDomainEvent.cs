using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;

namespace OzonEdu.MerchandiseApi.Domain.Events
{
    /// <summary>
    /// MercPack готов к выдаче
    /// </summary>
    public class MerchPackReadyDeliveryDomainEvent : INotification
    {
        public MerchPack MerchPack { get; }

        public MerchPackReadyDeliveryDomainEvent(MerchPack merchPack)
        {
            MerchPack = merchPack;
        }
    }
}