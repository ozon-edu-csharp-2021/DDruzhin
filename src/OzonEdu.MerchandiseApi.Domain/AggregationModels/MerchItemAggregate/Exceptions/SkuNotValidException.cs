using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions
{
    public class SkuNotValidException : Exception
    {
        public SkuNotValidException(string message) : base(message)
        {
            
        }
    }
}