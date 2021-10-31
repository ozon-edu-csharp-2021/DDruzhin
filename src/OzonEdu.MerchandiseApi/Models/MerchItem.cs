namespace OzonEdu.MerchandiseApi.Models
{
    public class MerchItem
    {
        public MerchItem(long merchItemId, MerchType merchType, long workerId, bool issued)
        {
            MerchItemId = merchItemId;
            MerchType = merchType;
            Issued = issued;
            WorkerId = workerId;
        }

        public long MerchItemId { get; }
        public MerchType MerchType { get; }
        public long WorkerId { get; }
        public bool Issued { get; }
    }
}