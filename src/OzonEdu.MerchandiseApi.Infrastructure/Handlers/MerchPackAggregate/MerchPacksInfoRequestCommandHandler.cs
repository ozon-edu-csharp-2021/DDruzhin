using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenTracing;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPacksInfoRequest;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate
{
    public class
        MerchPacksInfoRequestCommandHandler : IRequestHandler<MerchPacksInfoRequestCommand, IEnumerable<MerchPack>>
    {
        private readonly ITracer _tracer;
        private readonly IMerchPackRepository _merchPackRepository;

        public MerchPacksInfoRequestCommandHandler(ITracer tracer,IMerchPackRepository merchPackRepository)
        {
            _tracer = tracer;
            _merchPackRepository = merchPackRepository;
        }

        public async Task<IEnumerable<MerchPack>> Handle(MerchPacksInfoRequestCommand request,
            CancellationToken cancellationToken)
        {
            using var span = _tracer
                .BuildSpan("MerchPacksInfoRequestCommandHandler.Handle")
                .StartActive();
            return await _merchPackRepository.FindByWorkerEmailAsync(request.Worker, cancellationToken);
        }
    }
}