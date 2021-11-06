using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions
{
    public class AvailabilityNotValidException : Exception
    {
        public AvailabilityNotValidException(string message) : base(message)
        {
            
        }
    }
}