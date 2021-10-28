namespace OzonEdu.MerchandiseApi.Models
{
    public class MerchItemResponse
    {
        public long MerchItemId { get; set; }
        public MerchType MerchType { get; set; }
        public long WorkerId { get; set; }
        public bool Issued { get; set; }
    }
}