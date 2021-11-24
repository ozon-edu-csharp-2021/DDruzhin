using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Models;

namespace OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities
{
    public class MerchPack : Entity
    {
        public MerchPack(
            MerchType type, 
            IEnumerable<MerchItem> merchItems, 
            Worker worker, 
            DateTime requestDate, 
            DateTime deliveryDate, 
            Status status)
        {
            Type = type;
            MerchItems = merchItems;
            Worker = worker;
            SetRequestDate(requestDate);
            SetDeliveryDate(deliveryDate);
            SetStatus(status);
        }

        public void SetId(int id)
        {
            base.Id = id;
        }
        
        public void SetDeliveryDate(DateTime deliveryDate)
        {
            //TODO добавить проверки на дату
            DeliveryDate = deliveryDate;
        }

        private void SetStatus(Status status)
        {
            Status = status;
        }

        private void SetRequestDate(DateTime requestDate)
        {
            //TODO добавить проверки на дату
            RequestDate = requestDate;
        }

        // работник на которого выдается пак
        // что тут вообще забыл работник объясняется ниже
        public Worker Worker { get; }

        // техущий статус выдачи
        public Status Status { get; private set;}

        // тип пака
        public MerchType Type { get; }

        // итемы входящие в текущий пак, хранятся отдельно
        // для экземляра пака так как с течением времени
        // состав пака может менятся и если в этот момент будут
        // висеть незакрытые заявки на выдачу, то нарушится 
        // консистентность, а также для каждого работника необходимы
        // свои размеры одежды, которые удобнее хранить как отдельные
        // итемы, еще в будущем возможно расширение функционала
        // по типу предложения выбора состава пака сотруднику
        // например рюкзак или толстовка
        public IEnumerable<MerchItem> MerchItems { get; }

        // дата создания заявки на выдачу, если ведении очередности по
        // id в бд не подходит, то можно использовать это поле для
        // определения очередности
        public DateTime RequestDate { get; private set; }

        // дата готовности к выдаче пака, можно потом использовать для
        // анализа причин задержек тех или иных итемов
        public DateTime DeliveryDate { get; private set; }
    }
}