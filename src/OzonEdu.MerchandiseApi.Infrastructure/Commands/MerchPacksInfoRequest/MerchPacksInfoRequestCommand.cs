using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;

namespace OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPacksInfoRequest
{
    public class MerchPacksInfoRequestCommand : IRequest<IEnumerable<MerchPack>>
    {
        public string Worker { get; set; }
    }
}