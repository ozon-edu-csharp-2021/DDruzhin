using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Models;
using OzonEdu.MerchandiseApi.Services.Interfaces;

namespace OzonEdu.MerchandiseApi.Services
{
    public class MerchandiseService :IMerchandiseService
    {
        private readonly List<MerchItem> _merchItems = new()
        {
            new MerchItem(1,MerchType.VeteranPack,5,false),
            new MerchItem(2,MerchType.WelcomePack,3,true),
            new MerchItem(3,MerchType.ConferenceListenerPack,2,false),
            new MerchItem(4,MerchType.ConferenceSpeakerPack,4,true),
            new MerchItem(5,MerchType.ProbationPeriodEndingPack,1,false),
            new MerchItem(6,MerchType.VeteranPack,5,true),

        };
        public Task<MerchItem> RequestMerch(long workerId, MerchType merchType, CancellationToken token)
        {
            var itemId = _merchItems.Max(x => x.MerchItemId) + 1;
            var item = new MerchItem(itemId, merchType, workerId, false);
            _merchItems.Add(item);
            return Task.FromResult(item);
        }

        public Task<List<MerchItem>> RequestMerchInfo(long workerId, CancellationToken token)
        {
            var list = _merchItems.Where(item => item.WorkerId == workerId).ToList();
            return Task.FromResult(list);
        }
    }
}