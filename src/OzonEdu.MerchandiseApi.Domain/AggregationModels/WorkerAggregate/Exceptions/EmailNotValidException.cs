using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Exceptions
{
    public class EmailNotValidException : Exception
    {
        public EmailNotValidException(string message) : base(message)
        {
            
        }
    }
}