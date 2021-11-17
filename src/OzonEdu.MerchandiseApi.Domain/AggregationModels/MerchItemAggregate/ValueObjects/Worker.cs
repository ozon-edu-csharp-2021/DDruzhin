using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects
{
    public class Worker : Entity
    {
        // почта для отправки уведомления о готовности выдачи
        public Email Email { get; private set; }

        public Worker(Email email)
        {
            SetEmail(email);
        }

        private void SetEmail(Email value)
        {
            if (!value.Value.Contains('@'))
            {
                throw new EmailNotValidException($"{nameof(value)} is not Email");
            }

            Email = value;
        }
    }
}