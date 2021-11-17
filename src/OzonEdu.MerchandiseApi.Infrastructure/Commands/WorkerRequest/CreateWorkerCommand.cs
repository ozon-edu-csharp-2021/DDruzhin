using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;

namespace OzonEdu.MerchandiseApi.Infrastructure.Commands.WorkerRequest
{
    public class CreateWorkerCommand : IRequest<Worker>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}