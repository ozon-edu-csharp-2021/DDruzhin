using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;

namespace OzonEdu.MerchandiseApi.Infrastructure.KafkaProducers
{
    public interface IEmailNotificationProducer
    {
        void Publish(MerchPack merchPack);
    }
}