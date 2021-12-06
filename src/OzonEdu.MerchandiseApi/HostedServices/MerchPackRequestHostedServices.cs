using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using Microsoft.Extensions.Hosting;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;

namespace OzonEdu.MerchandiseApi.HostedServices;

public class MerchPackRequestHostedServices : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly IConsumer<long, NotificationEvent> _consumer;

    public MerchPackRequestHostedServices(IMediator mediator, IConsumer<long, NotificationEvent> consumer)
    {
        _mediator = mediator;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        _consumer.Subscribe("employee_notification_event");
        while (!stoppingToken.IsCancellationRequested)
        {
            var mes = _consumer.Consume(stoppingToken);
            if (mes is null) continue;

            var merchPackRequestCommand = new MerchPackRequestCommand
            {
                Worker = mes.Message.Value.EmployeeEmail,
                MerchItems = ((IEnumerable<long>) mes.Message.Value.Payload).Skip(1),
                MerchType = (int) ((IEnumerable<long>) mes.Message.Value.Payload).First()
            };
            await _mediator.Send(merchPackRequestCommand, stoppingToken);
            _consumer.Commit();
        }

        _consumer.Unsubscribe();
    }
}