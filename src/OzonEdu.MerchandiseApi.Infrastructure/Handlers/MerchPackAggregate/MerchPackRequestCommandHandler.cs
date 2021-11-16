using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.WorkerRequest;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate
{
    public class MerchPackRequestCommandHandler : IRequestHandler<MerchPackRequestCommand, MerchPack>
    {
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMerchItemRepository _merchItemRepository;
        private readonly IMediator _mediator;

        public MerchPackRequestCommandHandler(IMerchPackRepository merchPackRepository, IMediator mediator, IMerchItemRepository merchItemRepository)
        {
            _merchPackRepository = merchPackRepository;
            _mediator = mediator;
            _merchItemRepository = merchItemRepository;
        }

        public async Task<MerchPack> Handle(MerchPackRequestCommand request, CancellationToken cancellationToken)
        {
            await _merchItemRepository.CreateMerchItemsAsync(request.MerchItems, cancellationToken);
            await _merchItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var newMerchPack = new MerchPack(
                new MerchType(
                    request.MerchType,
                    Enumeration
                        .GetAll<MerchType>()
                        .FirstOrDefault(type => type.Id == request.MerchType)?.Name),
                request.MerchItems
                    .Select(l => new MerchItem(new Sku(l))) as List<MerchItem>,
                new Worker(new Email(request.Worker))
            );

            var createResult = await _merchPackRepository.CreateMerchPackAsync(newMerchPack, cancellationToken);

            await _merchPackRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return createResult;
        }
    }
}