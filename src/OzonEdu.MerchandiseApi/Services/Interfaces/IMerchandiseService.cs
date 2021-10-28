using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Models;

namespace OzonEdu.MerchandiseApi.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<MerchItem> RequestMerch(long workerId, MerchType merchType, CancellationToken token);
        Task<List<MerchItem>> RequestMerchInfo(long workerId, CancellationToken token);
    }
}