using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.WorkerAggregate.ValueObjects;
using Xunit;

namespace OzonEdu.MerchandiseApi.Domain.Tests
{
    public class MerchPackTests
    {
        private delegate void CreateMerchPack();
        
        [Fact]
        public void CreateMerchPackSuccess()
        {
            //Arrange
            var merchPack = new MerchPack(
                MerchType.WelcomePack,
                new List<MerchItem>
                {
                    new(new Sku(4564816)),
                    new(new Sku(54564564))
                },
                new Worker(
                    new Email("fsdf@asdas.sd"),
                    new WorkerName("Иван", "Петров"))

            );

            //Act

            //Assert
            Assert.NotNull(merchPack);
        }

        [Fact]
        public void CreateMerchPackWithWorkerNameNotValidFail()
        {
            //Arrange
            CreateMerchPack createMerchPack = () =>
            {
                var merchPack = new MerchPack(
                    MerchType.VeteranPack,
                    new List<MerchItem>
                    {
                        new(new Sku(4564816)),
                        new(new Sku(54564564))
                    },
                    new Worker(
                        new Email("fsdf@asdas.sd"),
                        new WorkerName("И", "Петров"))

                );
            };

            //Act

            //Assert
            Assert.Throws<NameNotValidException>(() => createMerchPack.Invoke());
        }

        [Fact]
        public void CreateMerchPackWithSkuNotValidFail()
        {
            //Arrange
            CreateMerchPack createMerchPack = () =>
            {
                var merchPack = new MerchPack(
                    MerchType.ConferenceListenerPack,
                    new List<MerchItem>
                    {
                        new(new Sku(0)),
                        new(new Sku(54564564))
                    },
                    new Worker(
                        new Email("fsdf@asdas.sd"),
                        new WorkerName("Иван", "Петров"))
                );
            };

            //Act

            //Assert
            Assert.Throws<SkuNotValidException>(() => createMerchPack.Invoke());
        }

        [Fact]
        public void CreateMerchPackWithEmailNotValidFail()
        {
            //Arrange
            CreateMerchPack createMerchPack = () =>
            {
                var merchPack = new MerchPack(
                    MerchType.ProbationPeriodEndingPack,
                    new List<MerchItem>
                    {
                        new(new Sku(4564816)),
                        new(new Sku(54564564))
                    },
                    new Worker(
                        new Email("fsdfasdas.sd"),
                        new WorkerName("Иван", "Петров"))

                );
            };

            //Act

            //Assert
            Assert.Throws<EmailNotValidException>(() => createMerchPack.Invoke());
        }
    }
}