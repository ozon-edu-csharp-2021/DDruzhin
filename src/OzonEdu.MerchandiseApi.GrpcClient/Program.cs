using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using OzonEdu.MerchandiseApi.Grpc;

namespace OzonEdu.MerchandiseApi.GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new MerchandiseApiGrpc.MerchandiseApiGrpcClient(channel);
            var response = await client.RequestMerchInfoAsync(new RequestMerchInfoRequest(),cancellationToken:CancellationToken.None);

            foreach (var item in response.Merch)
            {
                Console.WriteLine($"{item.MerchType} - {item.WorkerId} - {item.Issued}");
            }
        }
    }
}