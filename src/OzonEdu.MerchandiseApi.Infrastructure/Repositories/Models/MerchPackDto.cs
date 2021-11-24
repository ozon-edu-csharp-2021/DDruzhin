using System;
using System.Collections.Generic;

namespace OzonEdu.MerchandiseApi.Infrastructure.Repositories.Models
{
    public class MerchPackDto
    {
        public string WorkerEmail { get; set; }
        public int Status { get; set; }
        public int MerchType { get; set; }
        public IEnumerable<long> Items { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}