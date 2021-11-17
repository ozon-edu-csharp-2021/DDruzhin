using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;

namespace OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest
{
    public class MerchPackRequestCommand : IRequest<MerchPack>
    {
        public int MerchType { get; set; }
        public string Worker { get; set; }
        public IEnumerable<long> MerchItems { get; set; }
    }
}