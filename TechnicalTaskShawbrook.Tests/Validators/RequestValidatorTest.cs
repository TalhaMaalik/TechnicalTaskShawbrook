using BusinessLayer.DTOs;
using BusinessLayer.Messages;
using BusinessLayer.Validator;
using DataAccessLayer.Data;
using Moq;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Validators
{
    public class RequestValidatorTest
    {
        private readonly RequestValidator _Sut;
        private readonly Mock<ICustomerRepository> _CustomerRepoMock = new Mock<ICustomerRepository>();
        private readonly Mock<IItemRepository> _ItemRepoMock = new Mock<IItemRepository>();
        public RequestValidatorTest()
        {
            _Sut = new RequestValidator(_CustomerRepoMock.Object, _ItemRepoMock.Object);
        }

        #region ValidateCustomerEmailExists
        [Fact]
        public void ValidateCustomerEmailExists_ShouldThrowError_WhenCustomerExists()
        {
            //Arrange
            var email = "test@test.com";
            _CustomerRepoMock.Setup(x => x.CustomerExistsByEmail(email)).Returns(true);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.ValidateCustomerEmailExists(email));

            //Assert
            Assert.Equal(Message.CustomerWithEmailExistInSystem, ex.Message, true);
        }

        [Fact]
        public void ValidateCustomerEmailExists_ShouldWork_WhenCustomerDoesNotExists()
        {
            //Arrange
            var email = "test@test.com";
            _CustomerRepoMock.Setup(x => x.CustomerExistsByEmail(email)).Returns(false);

            //Act && Assert
            _Sut.ValidateCustomerEmailExists(email);

        }

        [Fact]
        public void ValidateCustomerEmailExists_ShouldWork_WhenCustomerIsNull()
        {
            //Arrange
            string email = null;
            _CustomerRepoMock.Setup(x => x.CustomerExistsByEmail(email)).Returns(false);

            //Act && Assert
            _Sut.ValidateCustomerEmailExists(email);
        }

        #endregion
        #region ValidateCustomerCreateDTO
        [Fact]
        public void ValidateCustomerCreateDTO_ShouldWork_WhenInputsAreGiven()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Name = "Talha Malik", Email = "test@test.com" };

            //Act & Assert
            _Sut.Validate(customerDTO);
        }

        [Fact]
        public void ValidateCustomerCreateDTO_ShouldThrowError_WhenCustomerEmailIsNotCorrect()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Name = "Talha Malik", Email = "talha" };

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(customerDTO));

            //Assert
            Assert.Equal(Message.CustomerEmailIsNotAValidEmail + "\r\n", ex.Message, true);

        }


        [Fact]
        public void ValidateCustomerCreateDTO_ShouldThrowError_WhenCustomerEmailIsEmpty()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Name = "Talha Malik", Email = string.Empty };

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(customerDTO));

            //Assert
            Assert.Equal(Message.CustomerEmailCannotBeEmpty + "\r\n" + Message.CustomerEmailIsNotAValidEmail + "\r\n", ex.Message, true);

        }

        [Fact]
        public void ValidateCustomerCreateDTO_ShouldThrowError_WhenCustomerEmailIsNotGiven()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Name = "Talha Malik" };

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(customerDTO));

            //Assert
            Assert.Equal(Message.CustomerEmailCannotBeNull + "\r\n" + Message.CustomerEmailCannotBeEmpty + "\r\n", ex.Message, true);

        }

        [Fact]
        public void ValidateCustomerCreateDTO_ShouldThrowError_WhenCustomerNameIsEmpty()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Email = "test@test.com", Name = string.Empty };

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(customerDTO));

            //Assert
            Assert.Equal(Message.CustomerNameCannotBeEmpty + "\r\n", ex.Message, true);
        }

        [Fact]
        public void ValidateCustomerCreateDTO_ShouldThrowError_WhenCustomerObjectIsNull()
        {
            //Arrange
            CustomerCreateDTO customerDTO = null;

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(customerDTO));

            //Assert
            Assert.Equal(Message.PleaseProvideTheCustomerInTheRequest, ex.Message, true);
        }

        [Fact]
        public void ValidateCustomerCreateDTO_ShouldThrowError_WhenCustomerNameIsNotGiven()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Email = "test@test.com" };

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(customerDTO));

            //Assert
            Assert.Equal(Message.CustomerNameCannotBeNull + "\r\n" + Message.CustomerNameCannotBeEmpty + "\r\n", ex.Message, true);
        }

        #endregion
        #region ValidateCustomerId

        [Fact]
        public void ValidateCustomerId_ShouldNotThrowError_WhenCustomerExist()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(customerId))
                .Returns(true);

            //Act & Assert
            _Sut.ValidateCustomerId(customerId);
        }
        [Fact]
        public void ValidateCustomerId_ShouldThrowError_WhenCustomerDoesnotExist()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(customerId))
                .Returns(false);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.ValidateCustomerId(customerId));

            //Assert
            Assert.Equal(Message.CustomerDoesNotExist, ex.Message);
        }

        [Fact]
        public void ValidateCustomerId_ShouldThrowError_EmptyGuid()
        {
            //Arrange
            var customerId = Guid.Empty;

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.ValidateCustomerId(customerId));

            //Assert
            Assert.Equal(Message.CustomerDoesNotExist, ex.Message);
        }

        [Fact]
        public void ValidateCustomerId_ValidCustomerId_CallsCustomerRepositoryWithCorrectId()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            _CustomerRepoMock.Setup(x => x.CustomerExists(customerId)).Returns(true);

            //Act
            _Sut.ValidateCustomerId(customerId);

            //Assert
            _CustomerRepoMock.Verify(x => x.CustomerExists(customerId), Times.Once);
        }

        [Fact]
        public void ValidateCustomerId_CustomerRepositoryThrowsException_ThrowsSameException()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            _CustomerRepoMock.Setup(x => x.CustomerExists(customerId)).Throws(new Exception("Test exception"));

            //Act
            var ex = Assert.Throws<Exception>(() => _Sut.ValidateCustomerId(customerId));

            //Assert
            Assert.Equal("Test exception", ex.Message);
        }

        #endregion
        #region ValidateItemsId

        [Fact]
        public void ValidateItemsId_ShowThrowError_WhenItemDoesnotExist()
        {
            // Arrange
            var items = GenerateItems();
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(false);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.ValidateItemsId(items));

            // Assert
            Assert.Equal(Message.ItemDoesNotExist, ex.Message);
        }

        [Fact]
        public void ValidateItemsId_ShouldNotThrowEror_WhenItemExists()
        {
            // Arrange
            var items = GenerateItems();
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(true);

            // Act & Assert
            _Sut.ValidateItemsId(items);
        }

        [Fact]
        public void ValidateItemsId_ShouldThrowEror_WhenItemListEmptyExists()
        {
            // Arrange
            var items = new List<ItemCreateDTO>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _Sut.ValidateItemsId(items));

        }

        #endregion
        #region ValidatePurchaseOrderCreateDTO

        [Fact]
        public void ValidatePurchaseOrderCreateDTO_ShouldWork_WhenInputsAreCorrectGiven()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = GenerateItems() };
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(purchaseOrderDTO.CustomerId))
                .Returns(true);
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(true);
            //Act & Assert
            _Sut.Validate(purchaseOrderDTO);
        }

        [Fact]
        public void ValidatePurchaseOrderCreateDTO_ShouldThrowError_WhenCustomerIdIsNotGiven()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { Items = GenerateItems() };
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(purchaseOrderDTO.CustomerId))
                .Returns(false);
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(true);
            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(purchaseOrderDTO));

            //Assert
            Assert.Equal(Message.CustomerDoesNotExist, ex.Message);
        }

        [Fact]
        public void ValidatePurchaseOrderCreateDTO_ShouldThrowError_WhenItemIsNotGiven()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid() };
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(purchaseOrderDTO.CustomerId))
                .Returns(false);
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(true);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(purchaseOrderDTO));

            //Assert
            Assert.Equal(Message.ItemAreNotGiven + "\r\n" + Message.ItemListIsEmpty + "\r\n", ex.Message);
        }

        [Fact]
        public void ValidatePurchaseOrderCreateDTO_ShouldThrowError_WhenCustomerDoesnotExist()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = GenerateItems() };
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(purchaseOrderDTO.CustomerId))
                .Returns(false);
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(true);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(purchaseOrderDTO));

            //Assert
            Assert.Equal(Message.CustomerDoesNotExist, ex.Message);
        }

        [Fact]
        public void ValidatePurchaseOrderCreateDTO_ShouldThrowError_WhenItemDoesnotExist()
        {
            //Arrange
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = Guid.NewGuid(), Items = GenerateItems() };
            _CustomerRepoMock
                .Setup(x => x.CustomerExists(purchaseOrderDTO.CustomerId))
                .Returns(true);
            _ItemRepoMock
                .Setup(x => x.ItemExists(It.IsAny<Guid>()))
                .Returns(false);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => _Sut.Validate(purchaseOrderDTO));

            //Assert
            Assert.Equal(Message.ItemDoesNotExist, ex.Message);
        }
        #endregion

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
