using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OzonEdu.MerchandiseApi.Grpc;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPacksInfoRequest;
using ILogger = Serilog.ILogger;
using Status = OzonEdu.MerchandiseApi.Grpc.Status;

namespace OzonEdu.MerchandiseApi.GrpcServices
{
    public class MerchandiseApiGrpService : MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public MerchandiseApiGrpService(ITracer tracer,IMediator mediator)
        {
            _tracer = tracer;
            _mediator = mediator;
        }

        public override async Task<RequestMerchPackResponse> RequestMerchPack(
            RequestMerchPackRequest request,
            ServerCallContext context)
        {
            using var span = _tracer
                .BuildSpan("MerchandiseApiGrpService.RequestMerchPack")
                .StartActive();
            var merchPackRequestCommand = new MerchPackRequestCommand
            {
                Worker = request.WorkerEmail,
                MerchItems = request.Items,
                MerchType = (int) request.MerchType
            };
            var merchPack = await _mediator.Send(merchPackRequestCommand, context.CancellationToken);
            return new RequestMerchPackResponse
            {
                MerchUnit =
                {
                    WorkerEmail = merchPack.Worker.Email.Value,
                    MerchPackId = merchPack.Id,
                    Status = (Status) merchPack.Status.Id,
                    DeliveryDate = merchPack.DeliveryDate.ToBinary(),
                    RequestDate = merchPack.RequestDate.ToBinary(),
                    MerchType = (MerchType) merchPack.Type.Id,
                    MerchItems = {merchPack.MerchItems.Select(item => item.Sku.Value).ToArray()}
                }
            };
        }

        public override async Task<RequestMerchPacksInfoResponse> RequestMerchPacksInfo(
            RequestMerchPacksInfoRequest request,
            ServerCallContext context)
        {
            using var span = _tracer
                .BuildSpan("MerchandiseApiGrpService.RequestMerchPacksInfo")
                .StartActive();
            var merchPacksInfoRequestCommand = new MerchPacksInfoRequestCommand
            {
                Worker = request.WorkerEmail
            };
            var merchPacks = await _mediator.Send(merchPacksInfoRequestCommand, context.CancellationToken);
            return new RequestMerchPacksInfoResponse
            {
                Merch =
                {
                    merchPacks.Select(pack => new MerchPackUnit
                    {
                        WorkerEmail = pack.Worker.Email.Value,
                        MerchPackId = pack.Id,
                        Status = (Status) pack.Status.Id,
                        DeliveryDate = pack.DeliveryDate.ToBinary(),
                        RequestDate = pack.RequestDate.ToBinary(),
                        MerchType = (MerchType) pack.Type.Id,
                        MerchItems = {pack.MerchItems.Select(item => item.Sku.Value).ToArray()}
                    })
                }
            };
        }
    }
}