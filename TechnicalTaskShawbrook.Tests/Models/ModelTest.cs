using BusinessLayer.Models.Items;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Models
{
    public class ModelTest
    {
        [Fact]
        public void PhysicalProduct_GetShippingSlip_ShouldReturnCorrectSlip()
        {
            //Arrange
            var item = new Book(Guid.NewGuid(), "Test", 5);
            var strategy = new KeyValueShippingSlip();

            //Act
            var result = item.GetShippingSlip(strategy);

            //Assert
            Assert.Equal(result, $"\"Name\" : {item.Name}, \"Cost\" : {item.Cost} \n");
        }
    }
}
