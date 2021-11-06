using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities
{
    public class MerchItem : Entity
    {
        // еденица мерча на складе
        public Sku Sku { get; private set; }

        // доступность этой единицы в момент запроса
        public bool Availability { get; private set; }

        public MerchItem(Sku sku)
        {
            SetSku(sku);
        }

        // при создании заявки доступность всех элементов
        // false по умолчанию, а потом при опросе
        // stock-api изменяется для итемов в наличии 
        public void ChangeAvailability()
        {
            if (Availability is true)
            {
                throw new AvailabilityNotValidException($"Availability cannot change to the same value");
            }

            Availability = true;
        }

        private void SetSku(Sku value)
        {
            if (value.Value <= 0)
            {
                throw new SkuNotValidException($"{nameof(value)} sku is negative or 0");
            }

            Sku = value;
        }
    }
}