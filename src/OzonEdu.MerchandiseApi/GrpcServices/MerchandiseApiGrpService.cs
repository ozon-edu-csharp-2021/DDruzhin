using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.MerchandiseApi.Grpc;
using OzonEdu.MerchandiseApi.Services.Interfaces;
using MerchType = OzonEdu.MerchandiseApi.Grpc.MerchType;

namespace OzonEdu.MerchandiseApi.GrpcServices
{
    public class MerchandiseApiGrpService : MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly IMerchandiseService _merchandiseService;

        public MerchandiseApiGrpService(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }

        public override async Task<RequestMerchResponse> RequestMerch(
            RequestMerchRequest request,
            ServerCallContext context)
        {
            var merchItem = await _merchandiseService.RequestMerch(request.WorkerId,
                (Models.MerchType) request.MerchType, context.CancellationToken);
            return new RequestMerchResponse
            {
                MerchUnit = new MerchUnit
                {
                    Issued = merchItem.Issued,
                    MerchType = (MerchType) merchItem.MerchType,
                    WorkerId = merchItem.WorkerId,
                    MerchItemId = merchItem.MerchItemId
                }
            };
        }

        public override async Task<RequestMerchInfoResponse> RequestMerchInfo(
            RequestMerchInfoRequest request,
            ServerCallContext context)
        {
            var merchItem = await _merchandiseService.RequestMerchInfo(request.WorkerId, context.CancellationToken);
            return new RequestMerchInfoResponse
            {
                Merch =
                {
                    merchItem.Select(x => new MerchUnit
                    {
                        Issued = x.Issued,
                        MerchType = (MerchType) x.MerchType,
                        WorkerId = x.WorkerId,
                        MerchItemId = x.MerchItemId
                    })
                }
            };
        }
    }
}