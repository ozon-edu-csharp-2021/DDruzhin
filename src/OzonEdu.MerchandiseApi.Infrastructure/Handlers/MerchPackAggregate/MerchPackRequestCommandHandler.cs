using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenTracing;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Contracts;
using OzonEdu.MerchandiseApi.Domain.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.KafkaProducers;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate
{
    public class MerchPackRequestCommandHandler : IRequestHandler<MerchPackRequestCommand, MerchPack>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracer _tracer;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMerchItemRepository _merchItemRepository;
        private readonly IEmailNotificationProducer _emailNotificationProducer;

        public MerchPackRequestCommandHandler(ITracer tracer,IEmailNotificationProducer producer,IMerchPackRepository merchPackRepository, IMerchItemRepository merchItemRepository, IUnitOfWork unitOfWork)
        {
            _tracer = tracer;
            _merchPackRepository = merchPackRepository;
            _merchItemRepository = merchItemRepository;
            _unitOfWork = unitOfWork;
            _emailNotificationProducer = producer;
        }

        public async Task<MerchPack> Handle(MerchPackRequestCommand request, CancellationToken cancellationToken)
        {
            using var span = _tracer
                .BuildSpan("MerchPackRequestCommandHandler.Handle")
                .StartActive();
            await _unitOfWork.StartTransaction(cancellationToken);
            
            var merchItems = await _merchItemRepository.CreateMerchItemsAsync(request.MerchItems.Select(l =>  new MerchItem(new Sku(l))), cancellationToken);
            
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

            // проверяем можем ли мы сразу выдать пак
            await RequestMerchItems(createResult, cancellationToken);
            
            return createResult;
        }
        
        private async Task RequestMerchItems(MerchPack merchPack, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);

            foreach (var item in merchPack.MerchItems)
            {
                // проверка нужна при новой поставке
                if (item.Availability) continue;
                
                //TODO тут запрос к stock-api по sku на доступность итема
                // если доступен, то резервируем

                // и изменяем статус итема
                item.ChangeAvailability();
                await _merchItemRepository.UpdateAsync(item, cancellationToken);
            }
            
            // если все итемы для выдачи есть и зарезервированы
            if (merchPack.MerchItems.Count(item => item.Availability is true) == merchPack.MerchItems.Count() && merchPack.MerchItems.Any())
            {
                merchPack.SetDeliveryDate(DateTime.Now);

                await _merchPackRepository.UpdateAsync(merchPack, cancellationToken);
                
                _emailNotificationProducer.Publish(merchPack);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}