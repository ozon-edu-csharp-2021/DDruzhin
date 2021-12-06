using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace OzonEdu.MerchandiseApi.HostedServices;

public class NewSupplyHostedServices : BackgroundService
{
    private readonly IConsumer<long, StockReplenishedEvent> _consumer;
    private readonly IMediator _mediator;

    public NewSupplyHostedServices(IConsumer<long, StockReplenishedEvent> consumer, IMediator mediator)
    {
        _consumer = consumer;
        _mediator = mediator;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        _consumer.Subscribe("stock_replenished_event");
        while (!stoppingToken.IsCancellationRequested)
        {
            var mes = _consumer.Consume(stoppingToken);
            if (mes is null) continue;
            //TODO дописать пересмотр

            _consumer.Commit();
        }

        _consumer.Unsubscribe();
    }
}