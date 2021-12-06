using Confluent.Kafka;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;

namespace OzonEdu.MerchandiseApi.Infrastructure.KafkaProducers
{
    public class EmailNotificationProducer : IEmailNotificationProducer
    {
        private readonly IProducer<long, NotificationEvent> _producer;

        public EmailNotificationProducer(IProducer<long, NotificationEvent> producer)
        {
            _producer = producer;
        }

        public void Publish(MerchPack merchPack)
        {
            var mes = new Message<long, NotificationEvent>
            {
                Key = merchPack.Id,
                Value = new NotificationEvent
                {
                    EmployeeEmail = merchPack.Worker.Email.Value,
                    EmployeeName = merchPack.Worker.Email.Value,
                    EventType = EmployeeEventType.MerchDelivery
                }
            };
            _producer.Produce("email_notification_event", mes);
        }
    }
}