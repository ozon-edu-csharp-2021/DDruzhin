using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchItemAggregate.ValueObjects;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
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
                new List<MerchItem>{
                    new(new Sku(4564816)),
                    new(new Sku(54564564))
                },
                new Worker(new Email("123@sad.sd")), 
                DateTime.Now, 
                DateTime.MinValue,
                Status.WaitItems);

            //Act

            //Assert
            Assert.NotNull(merchPack);
        }

        [Fact]
        public void CreateMerchPackWithSkuNotValidFail()
        {
            //Arrange
            CreateMerchPack createMerchPack = () =>
            {
                var merchPack = new MerchPack(
                    MerchType.WelcomePack, 
                    new List<MerchItem>{
                        new(new Sku(0)),
                        new(new Sku(54564564))
                    },
                    new Worker(new Email("123@sad.sd")), 
                    DateTime.Now, 
                    DateTime.MinValue,
                    Status.WaitItems);
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
                    MerchType.WelcomePack, 
                    new List<MerchItem>{
                        new(new Sku(4564816)),
                        new(new Sku(54564564))
                    },
                    new Worker(new Email("123sad.sd")), 
                    DateTime.Now, 
                    DateTime.MinValue,
                    Status.WaitItems);
            };

            //Act

            //Assert
            Assert.Throws<EmailNotValidException>(() => createMerchPack.Invoke());
        }
    }
}