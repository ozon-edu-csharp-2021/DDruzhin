﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate;
using OzonEdu.MerchandiseApi.Domain.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.WorkerRequest;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.MerchPackAggregate
{
    public class MerchPackRequestCommandHandler : IRequestHandler<MerchPackRequestCommand, MerchPack>
    {
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IMediator _mediator;

        public MerchPackRequestCommandHandler(IMerchPackRepository merchPackRepository,
            IWorkerRepository workerRepository, IMediator mediator)
        {
            _merchPackRepository = merchPackRepository;
            _workerRepository = workerRepository;
            _mediator = mediator;
        }

        public async Task<MerchPack> Handle(MerchPackRequestCommand request, CancellationToken cancellationToken)
        {
            var workerInBd = await _workerRepository.FindByEmailAsync(request.Worker, cancellationToken);
            if (workerInBd is null)
            {
               //TODO тут можно добавлять рабочего если его еще нет, но не знаю
               // является ли это ответственностью моего сервиса
               // но обработчик для этого навсякий написал
               
                throw new Exception($"Worker with email: {request.Worker} does not exist");
            }

            var newMerchPack = new MerchPack(
                new MerchType(
                    request.MerchType,
                    Enumeration
                        .GetAll<MerchType>()
                        .FirstOrDefault(type => type.Id == request.MerchType)?.Name),
                request.MerchItems
                    .Select(l => new MerchItem(new Sku(l))) as List<MerchItem>,
                workerInBd
            );

            var createResult = await _merchPackRepository.CreateMerchPackAsync(newMerchPack, cancellationToken);

            await _merchPackRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return createResult;
        }
    }
}