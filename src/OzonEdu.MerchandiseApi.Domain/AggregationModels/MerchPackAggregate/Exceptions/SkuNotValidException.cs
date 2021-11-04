using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Exceptions
{
    public class SkuNotValidException : Exception
    {
        public SkuNotValidException(string message) : base(message)
        {
            
        }
    }
}