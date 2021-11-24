using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Models.Enum;

namespace OzonEdu.MerchandiseApi.Models
{
    public class MerchPackResponse
    {
        public int Status { get; set; }
        public MerchType MerchType { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}