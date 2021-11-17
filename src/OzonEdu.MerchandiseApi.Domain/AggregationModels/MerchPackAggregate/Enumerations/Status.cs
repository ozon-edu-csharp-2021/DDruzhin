using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations
{
    //TODO неуверен как это будет потом завязываться на синхронизацию с БД
    public class Status : Enumeration
    {
        // накидал всего два, потом при наличии списка можно легко расширить
        public static Status Issued  = new (1, nameof(Issued));
        public static Status WaitItems  = new (2, nameof(WaitItems));

        public Status(int id, string name) : base(id, name)
        {
        }
    }
}