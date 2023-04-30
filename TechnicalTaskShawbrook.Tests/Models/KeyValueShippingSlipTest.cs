using BusinessLayer.Models.Items;
using BusinessLayer.Processors.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Models
{
    public class KeyValueShippingSlipTest
    {
        [Fact]
        public void GenerateShippingSlip_ShouldGenerate_CorrectSlip()
        {
            //Arrange
            var item = new Book(Guid.NewGuid(), "Test", 5);
            var strategy = new KeyValueShippingSlip();

            //Act
            var result = strategy.GenerateShippingSlip(item);

            //Assert
            Assert.Equal(result, $"\"Name\" : {item.Name}, \"Cost\" : {item.Cost} \n");
        }
    }
}
