using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using OzonEdu.MerchandiseApi.Grpc;
using MerchType = OzonEdu.MerchandiseApi.Grpc.MerchType;

namespace OzonEdu.MerchandiseApi.GrpcServices
{
    public class MerchandiseApiGrpService : MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly IMediator _mediator;
        public MerchandiseApiGrpService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<RequestMerchResponse> RequestMerch(
            RequestMerchRequest request,
            ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<RequestMerchInfoResponse> RequestMerchInfo(
            RequestMerchInfoRequest request,
            ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}