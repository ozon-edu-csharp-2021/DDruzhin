using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Enumerations;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Exceptions;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.ValueObjects;
using Xunit;

namespace OzonEdu.MerchandiseApi.Domain.Tests
{
    public class MerchItemTests
    {
        [Fact]
        public void ChangeAvailabilityMerchItemSuccess()
        {
            //Arrange
            var merchItem = new MerchItem(new Sku(54654));

            //Act
            merchItem.ChangeAvailability();
          
            //Assert
            Assert.True(merchItem.Availability);

        }
        
        [Fact]
        public void ChangeAvailabilityMerchItemFail()
        {
            //Arrange
            var merchItem = new MerchItem(new Sku(54654));

            //Act
            merchItem.ChangeAvailability();

            //Assert
            Assert.Throws<AvailabilityNotValidException>(merchItem.ChangeAvailability);

        }
    }
}