using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }
        
        public Email(string email)
        {
            Value = email;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}