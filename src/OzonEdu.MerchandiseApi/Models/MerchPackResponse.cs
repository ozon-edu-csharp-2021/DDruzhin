using System.Collections.Generic;

namespace OzonEdu.MerchandiseApi.Models
{
    public class MerchPackResponse
    {
        public long MerchPackId { get; set; }
        public MerchType MerchType { get; set; }
        public string WorkerEmail { get; set; }
        public IEnumerable<long> Items { get; set; }
    }
}