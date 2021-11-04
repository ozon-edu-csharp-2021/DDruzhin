using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.ValueObjects
{
    public class WorkerName : ValueObject
    {
        public WorkerName(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public string FirstName { get; }
        public string SecondName { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return SecondName;
        }
    }
}