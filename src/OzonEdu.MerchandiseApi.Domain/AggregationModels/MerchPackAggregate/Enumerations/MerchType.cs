using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations
{
    //TODO неуверен как это будет потом завязываться на синхронизацию с БД
    public class MerchType : Enumeration
    {
        public static MerchType WelcomePack = new(1, nameof(WelcomePack));
        public static MerchType ConferenceListenerPack = new(2, nameof(ConferenceListenerPack));
        public static MerchType ConferenceSpeakerPack = new(3, nameof(ConferenceSpeakerPack));
        public static MerchType ProbationPeriodEndingPack = new(4, nameof(ProbationPeriodEndingPack));
        public static MerchType VeteranPack  = new(5, nameof(VeteranPack));
        public MerchType(int id, string name) : base(id, name)
        {
        }
    }
}