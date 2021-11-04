using System;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Exceptions
{
    public class NameNotValidException : Exception
    {
        public NameNotValidException(string message) : base(message)
        {
            
        }
    }
}