using AutoMapper;
using BusinessLayer.Controllers;
using BusinessLayer.DTOs;
using BusinessLayer.Models.Base;
using BusinessLayer.Processor;
using BusinessLayer.Processors.Factory;
using BusinessLayer.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Controllers
{
    public class PurchaseOrderControllerTest
    {
        private readonly Mock<IValidator> _ValidatorMock = new Mock<IValidator>();
        private readonly Mock<IMapper> _MapperMock = new Mock<IMapper>();
        private readonly Mock<IItemFactory> _ItemFactoryMock = new Mock<IItemFactory>();
        private readonly Mock<IPurchaseOrderProcessor> _PurchaseOrderProcessorMock = new Mock<IPurchaseOrderProcessor>();
        private readonly PurchaseOrderController _Sut;

        public PurchaseOrderControllerTest()
        {
            _Sut = new PurchaseOrderController
                (_ValidatorMock.Object, _MapperMock.Object, _ItemFactoryMock.Object, _PurchaseOrderProcessorMock.Object);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnOk_WhenInputsAreCorrect()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Returns(new PurchaseOrder());
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(It.IsAny<Item>());
            _PurchaseOrderProcessorMock.Setup(x => x.Process(It.IsAny<PurchaseOrder>())).Returns("Test");

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 200);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenCustomerIdIsNotGiven()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { Items = items };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Returns(new PurchaseOrder());
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(It.IsAny<Item>());
            _PurchaseOrderProcessorMock.Setup(x => x.Process(It.IsAny<PurchaseOrder>())).Returns("Test");
            _ValidatorMock.Setup(x => x.Validate(purchaseOrderDTO)).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.Equal(objectResult.StatusCode, 400);
            _MapperMock.Verify(x => x.Map<PurchaseOrder>(purchaseOrderDTO), Times.Never);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldProcessAllItems_WhenInputsAreCorrect()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { Items = items };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Returns(new PurchaseOrder());
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(It.IsAny<Item>());

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.Equal(objectResult.StatusCode, 200);
            _ItemFactoryMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Exactly(3));
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenItemsIsNotGiven()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid() };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Returns(new PurchaseOrder());
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(It.IsAny<Item>());
            _PurchaseOrderProcessorMock.Setup(x => x.Process(It.IsAny<PurchaseOrder>())).Returns("Test");
            _ValidatorMock.Setup(x => x.Validate(purchaseOrderDTO)).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.Equal(objectResult.StatusCode, 400);
            _MapperMock.Verify(x => x.Map<PurchaseOrder>(purchaseOrderDTO), Times.Never);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenItemsListIsEmpty()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = new List<ItemCreateDTO>() };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Returns(new PurchaseOrder());
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(It.IsAny<Item>());
            _PurchaseOrderProcessorMock.Setup(x => x.Process(It.IsAny<PurchaseOrder>())).Returns("Test");
            _ValidatorMock.Setup(x => x.Validate(purchaseOrderDTO)).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.Equal(objectResult.StatusCode, 400);
            _MapperMock.Verify(x => x.Map<PurchaseOrder>(purchaseOrderDTO), Times.Never);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenValidationFails()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 400);
        }


        [Fact]
        public void CreatePurchaseOrder_ShouldReturnOkWithData_WhenInputsAreCorrect()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _MapperMock.Setup(x => x.Map<PurchaseOrder>(purchaseOrderDTO)).Returns(new PurchaseOrder());
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(It.IsAny<Item>());
            _PurchaseOrderProcessorMock.Setup(x => x.Process(It.IsAny<PurchaseOrder>())).Returns("Test");

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            var resultobject = objectResult.Value as string;
            Assert.Equal(resultobject, "Test");
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequestWithMessage_WhenInputsAreCorrect()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _ValidatorMock.Setup(x => x.Validate(purchaseOrderDTO)).Throws(new ArgumentException("Error Message"));

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            var resultobject = objectResult.Value as string;
            Assert.Equal(resultobject, "Error Message");
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenAutoMapperFails()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _ValidatorMock.Setup(x => x.Validate(purchaseOrderDTO)).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 400);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenFactoryFails()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _ItemFactoryMock.Setup(x => x.Create(It.IsAny<Guid>())).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 400);
        }

        [Fact]
        public void CreatePurchaseOrder_ShouldReturnBadRequest_WhenErrorProcessingItems()
        {
            //Arrange
            var items = GenerateItems();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = items };
            _PurchaseOrderProcessorMock.Setup(x => x.Process(It.IsAny<PurchaseOrder>())).Throws<ArgumentException>();

            //Act
            var result = _Sut.CreatePurchaseOrder(purchaseOrderDTO);

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 400);
        }
        private List<ItemCreateDTO> GenerateItems()
        {
            return new List<ItemCreateDTO>
            {
                new ItemCreateDTO { ItemId = Guid.NewGuid() },
                new ItemCreateDTO { ItemId = Guid.NewGuid() },
                new ItemCreateDTO { ItemId = Guid.NewGuid() }
            };
        }
    }
}
