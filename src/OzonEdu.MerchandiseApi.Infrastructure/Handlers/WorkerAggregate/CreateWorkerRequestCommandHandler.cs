using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.WorkerRequest;

namespace OzonEdu.MerchandiseApi.Infrastructure.Handlers.WorkerAggregate
{
    public class CreateWorkerRequestCommandHandler : IRequestHandler<CreateWorkerCommand, Worker>
    {
        private readonly IWorkerRepository _workerRepository;

        public CreateWorkerRequestCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }
        
        public async Task<Worker> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var workerInDb = await _workerRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (workerInDb is null)
            {
                var worker = new Worker(new Email(request.Email),
                    new WorkerName(request.FirstName, request.SecondName));

                return await _workerRepository.CreateAsync(worker, cancellationToken);
            }

            return workerInDb;
        }
    }
}