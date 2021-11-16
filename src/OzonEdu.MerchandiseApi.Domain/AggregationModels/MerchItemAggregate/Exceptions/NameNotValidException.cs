using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions
{
    public class NameNotValidException : Exception
    {
        public NameNotValidException(string message) : base(message)
        {
            
        }
    }
}