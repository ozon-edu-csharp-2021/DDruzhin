namespace OzonEdu.MerchandiseApi.Models.Enum
{
    public enum MerchType
    {
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при устройстве на работу.
        /// </summary>
        WelcomePack = 10,
    
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя.
        /// </summary>
        ConferenceListenerPack = 20,
    
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера.
        /// </summary>
        ConferenceSpeakerPack = 30,
    
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока.
        /// </summary>
        ProbationPeriodEndingPack = 40,
    
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику за выслугу лет.
        /// </summary>
        VeteranPack = 50
    }
}