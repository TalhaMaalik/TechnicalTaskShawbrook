using BusinessLayer.DTOs;
using BusinessLayer.Models.Base;
using BusinessLayer.Models.Items;
using BusinessLayer.Processor;
using BusinessLayer.Processors.Visitor;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TechnicalTaskShawbrook.Tests.Processors
{
    public class PurchaseOrderProcessorTest
    {
        private readonly Mock<IItemVisitor> _ItemProcessorMock = new();
        private readonly PurchaseOrderProcessor _Sut;

        public PurchaseOrderProcessorTest()
        {
            _Sut = new(_ItemProcessorMock.Object);
        }

        [Fact]
        public void Process_ShouldCallOnClose()
        {
            //Arrange
            var purchaseOrder = new PurchaseOrder() { CustomerId = Guid.NewGuid(), Items = GenerateItems() };
            _ItemProcessorMock.Setup(x => x.VisitItem(It.IsAny<Item>()));
            _ItemProcessorMock.Setup(x => x.ShippingSlip).Returns(new StringBuilder("test"));
            //Act
            _Sut.Process(purchaseOrder);

            //Assert
            _ItemProcessorMock.Verify(x => x.OnClose(), Times.Once);

        }

        [Fact]
        public void Process_ShouldReturnCorrectShippingSlip()
        {
            //Arrange
            var purchaseOrder = new PurchaseOrder() { CustomerId = Guid.NewGuid(), Items = GenerateItems() };
            _ItemProcessorMock.Setup(x => x.VisitItem(It.IsAny<Item>()));
            _ItemProcessorMock.Setup(x => x.ShippingSlip).Returns(new StringBuilder("test"));
            //Act
            var result = _Sut.Process(purchaseOrder);

            //Assert
            Assert.Equal(result, "test");

        }

        [Fact]
        public void Process_ShouldProcessAllitems()
        {
            //Arrange
            var purchaseOrder = new PurchaseOrder() { CustomerId = Guid.NewGuid(), Items = GenerateItems() };
            _ItemProcessorMock.Setup(x => x.VisitItem(It.IsAny<Item>()));
            _ItemProcessorMock.Setup(x => x.ShippingSlip).Returns(new StringBuilder("test"));
            //Act
            _Sut.Process(purchaseOrder);

            //Assert
            _ItemProcessorMock.Verify(x => x.VisitItem(It.IsAny<Item>()), Times.Exactly(3));

        }

        private List<Item> GenerateItems()
        {
            return new List<Item>
            {
                new Video(Guid.NewGuid(), "Test", 0),
                new Video(Guid.NewGuid(), "Test", 0),
                new Video(Guid.NewGuid(), "Test", 0)
            };
        }
    }
}
