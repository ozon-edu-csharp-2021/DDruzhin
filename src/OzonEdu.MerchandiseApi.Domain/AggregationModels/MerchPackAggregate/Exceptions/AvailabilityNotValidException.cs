using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Exceptions
{
    public class AvailabilityNotValidException : Exception
    {
        public AvailabilityNotValidException(string message) : base(message)
        {
            
        }
    }
}