using AutoMapper;
using Azure;
using BusinessLayer.Controllers;
using BusinessLayer.DTOs;
using BusinessLayer.Validator;
using Castle.Core.Resource;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace TechnicalTaskShawbrook.Tests
{
    public class CustomerControllerTest
    {
        private readonly CustomerController _Sut;
        private readonly Mock<ICustomerRepository> _CustomerRepoMock = new Mock<ICustomerRepository>();
        private readonly Mock<IValidator> _ValidatorMock = new Mock<IValidator>();
        private readonly Mock<IMapper> _MapperMock = new Mock<IMapper>();

        public CustomerControllerTest()
        {
            _Sut = new CustomerController(_CustomerRepoMock.Object, _ValidatorMock.Object, _MapperMock.Object);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnOk_WhenInputsAreCorrect()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerReadDTO>()))
                .Returns(new CustomerReadDTO());
            _CustomerRepoMock.Setup( x => x.CreateCustomer(It.IsAny<CustomerModel>()));
            _CustomerRepoMock.Setup(x => x.SaveChanges()).Returns(true);

            //Act
            var result = _Sut.Create(It.IsAny<CustomerCreateDTO>());

            //Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(okResult.StatusCode, 200);
            _ValidatorMock.Verify(x => x.Validate(It.IsAny<CustomerCreateDTO>()), Times.Once);
            _CustomerRepoMock.Verify(x => x.CreateCustomer(It.IsAny<CustomerModel>()), Times.Once);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnProblem_WhenServerError()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerReadDTO>()))
                .Returns(new CustomerReadDTO());
            _CustomerRepoMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerModel>()));
            _CustomerRepoMock.Setup(x => x.SaveChanges()).Returns(false);

            //Act
            var result = _Sut.Create(It.IsAny<CustomerCreateDTO>());

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 500);
            _ValidatorMock.Verify(x => x.Validate(It.IsAny<CustomerCreateDTO>()), Times.Once);
            _CustomerRepoMock.Verify(x => x.CreateCustomer(It.IsAny<CustomerModel>()), Times.Once);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenInputsAreIncorrect()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerReadDTO>()))
                .Returns(new CustomerReadDTO());
            _ValidatorMock.Setup(x => x.Validate(It.IsAny<CustomerCreateDTO>()))
                .Throws(new Exception());
            _CustomerRepoMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerModel>()));
            _CustomerRepoMock.Setup(x => x.SaveChanges()).Returns(true);

            //Act
            var result = _Sut.Create(It.IsAny<CustomerCreateDTO>());

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 400);
            _ValidatorMock.Verify(x => x.Validate(It.IsAny<CustomerCreateDTO>()), Times.Once);
            _CustomerRepoMock.Verify(x => x.CreateCustomer(It.IsAny<CustomerModel>()), Times.Never);
        }
    }
}
