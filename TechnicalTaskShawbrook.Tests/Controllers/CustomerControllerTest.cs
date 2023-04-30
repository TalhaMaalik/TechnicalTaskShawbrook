using AutoMapper;
using BusinessLayer.Controllers;
using BusinessLayer.DTOs;
using BusinessLayer.Messages;
using BusinessLayer.Validator;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Controllers
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
        public void CreateCustomer_ShouldReturnReadDTO_WhenInputsAreCorrect()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerModel>()))
                .Returns(new CustomerReadDTO());
            _CustomerRepoMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerModel>()));
            _CustomerRepoMock.Setup(x => x.SaveChanges()).Returns(true);

            //Act
            var result = _Sut.Create(It.IsAny<CustomerCreateDTO>());

            //Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsType<CustomerReadDTO>(okResult.Value);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenCustomerEmailIsNotCorrect()
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Name = "Talha Malik", Email = "test" };
            _ValidatorMock.Setup(x => x.Validate(customer))
                .Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenCustomerNameIsEmpty()
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Name = string.Empty, Email = "talha@test.com" };
            _ValidatorMock.Setup(x => x.Validate(customer))
                .Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void CreateCustomer_ShouldReturnBadRequest_WhenCustomerNameIsInvalid(string name)
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Name = name, Email = "talha@test.com" };
            _ValidatorMock.Setup(x => x.Validate(customer)).Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("a@b.c")]
        [InlineData("asdasdasd")]
        [InlineData("adasdasdasdasdasjdasjdkoasjdoasjdjasodkjasdkoasjdkoasj@gmail.com")]
        [InlineData(".com")]
        [InlineData("@.com")]
        public void CreateCustomer_ShouldReturnBadRequest_WhenEmailIsInvalid(string email)
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Name = "Talha", Email = email };
            _ValidatorMock.Setup(x => x.Validate(customer)).Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenCustomerEmailIsNotGiven()
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Name = "Talha Malik" };
            _ValidatorMock.Setup(x => x.Validate(customer))
                .Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenCustomerNameIsNotGiven()
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Email = "talha@test.com" };
            _ValidatorMock.Setup(x => x.Validate(customer))
                .Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenCustomerIsNull()
        {
            // Arrange
            _ValidatorMock.Setup(x => x.Validate(It.IsAny<CustomerCreateDTO>()))
                .Throws(new ArgumentException(Message.PleaseProvideTheCustomerInTheRequest));
            // Act
            var result = _Sut.Create(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenAutoMapperCannotMap()
        {
            // Arrange
            var customer = new CustomerCreateDTO() { Name = "Talha Malik", Email = " Talha@test.com" };
            _MapperMock.Setup(x => x.Map<CustomerModel>(customer))
                .Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenAutoMapperCannotMapResult()
        {
            // Arrange
            var customer = new CustomerModel() { Name = "Talha Malik", Email = "talha@test.com" };
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(customer))
                .Throws(new ArgumentException());

            // Act
            var result = _Sut.Create(It.IsAny<CustomerCreateDTO>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnOk_WhenInputsAreCorrect()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerModel>()))
                .Returns(new CustomerReadDTO());
            _CustomerRepoMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerModel>()));
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
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerModel>()))
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
        public void CreateCustomer_ShouldReturnBadRequest_WhenCannotCreateCustomer()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerModel>()))
                .Returns(new CustomerReadDTO());
            _ValidatorMock.Setup(x => x.Validate(It.IsAny<CustomerCreateDTO>()));
            _CustomerRepoMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerModel>())).Throws(new Exception()); ;
            _CustomerRepoMock.Setup(x => x.SaveChanges()).Returns(true);

            //Act
            var result = _Sut.Create(It.IsAny<CustomerCreateDTO>());

            //Assert
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, 400);
            _ValidatorMock.Verify(x => x.Validate(It.IsAny<CustomerCreateDTO>()), Times.Once);
            _CustomerRepoMock.Verify(x => x.CreateCustomer(It.IsAny<CustomerModel>()), Times.Once);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnBadRequest_WhenValidationFails()
        {
            //Arrange
            _MapperMock.Setup(x => x.Map<CustomerModel>(It.IsAny<CustomerCreateDTO>()))
                .Returns(new CustomerModel());
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(It.IsAny<CustomerModel>()))
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

        [Fact]
        public void CreateCustomer_ShouldReturnReadDTOWithSameValues_WhenInputsAreCorrect()
        {
            //Arrange
            var customerCreateDTO = new CustomerCreateDTO() { Name = "Talha Malik", Email = "Talha@test.com" };
            var customerReadDTO = new CustomerReadDTO() { Name = "Talha Malik", Email = "Talha@test.com" };
            var customerModel = new CustomerModel() { Name = "Talha Malik", Email = "Talha@test.com" };
            _MapperMock.Setup(x => x.Map<CustomerModel>(customerCreateDTO))
                .Returns(customerModel);
            _MapperMock.Setup(x => x.Map<CustomerReadDTO>(customerModel))
                .Returns(customerReadDTO);
            _CustomerRepoMock.Setup(x => x.CreateCustomer(It.IsAny<CustomerModel>()));
            _CustomerRepoMock.Setup(x => x.SaveChanges()).Returns(true);

            //Act
            var result = _Sut.Create(customerCreateDTO);

            //Assert
            var okResult = result.Result as OkObjectResult;
            var resultDTO = okResult.Value as CustomerReadDTO;
            Assert.Equal(customerCreateDTO.Name, resultDTO.Name);
            Assert.Equal(customerCreateDTO.Email, resultDTO.Email);
        }
    }
}
