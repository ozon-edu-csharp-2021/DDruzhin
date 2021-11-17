using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions
{
    public class EmailNotValidException : Exception
    {
        public EmailNotValidException(string message) : base(message)
        {
            
        }
    }
}