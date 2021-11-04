using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPacksInfoRequest;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate
{
    public class
        MerchPacksInfoRequestCommandHandler : IRequestHandler<MerchPacksInfoRequestCommand, IEnumerable<MerchPack>>
    {
        private readonly IMerchPackRepository _merchPackRepository;

        public MerchPacksInfoRequestCommandHandler(IMerchPackRepository merchPackRepository)
        {
            _merchPackRepository = merchPackRepository;
        }

        public async Task<IEnumerable<MerchPack>> Handle(MerchPacksInfoRequestCommand request,
            CancellationToken cancellationToken)
        {
            var merchPacks = await _merchPackRepository.FindByWorkerAsync(request.Worker, cancellationToken);
            return merchPacks ;
        }
    }
}