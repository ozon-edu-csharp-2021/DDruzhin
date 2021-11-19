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
using OzonEdu.MerchandiseApi.Domain.Contracts;
using OzonEdu.MerchandiseApi.Domain.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate
{
    public class MerchPackRequestCommandHandler : IRequestHandler<MerchPackRequestCommand, MerchPack>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMerchItemRepository _merchItemRepository;

        public MerchPackRequestCommandHandler(IMerchPackRepository merchPackRepository, IMerchItemRepository merchItemRepository, IUnitOfWork unitOfWork)
        {
            _merchPackRepository = merchPackRepository;
            _merchItemRepository = merchItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MerchPack> Handle(MerchPackRequestCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            
            var merchItems = new List<MerchItem>();
            
            //TODO возможно это лучше делать одним запросом
            foreach (var item in request.MerchItems)
            {
                merchItems.Add(await _merchItemRepository.CreateMerchItemAsync(new MerchItem(new Sku(item)), cancellationToken));
            }

            var newMerchPack = new MerchPack(
                new MerchType(request.MerchType, Enumeration.GetAll<MerchType>().FirstOrDefault(type => type.Id == request.MerchType)?.Name),
                merchItems,
                new Worker(new Email(request.Worker)),
                requestDate: DateTime.Now, 
                deliveryDate: DateTime.MinValue, 
                Status.WaitItems
            );

            var createResult = await _merchPackRepository.CreateMerchPackAsync(newMerchPack, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return createResult;
        }
    }
}