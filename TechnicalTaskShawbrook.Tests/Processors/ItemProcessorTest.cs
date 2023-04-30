using BusinessLayer.Messages;
using BusinessLayer.Models.Base;
using BusinessLayer.Models.Items;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Visitor;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;
using Moq;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Processors
{
    public class ItemProcessorTest
    {
        private readonly Mock<ICustomerRepository> _CustomerRepositoryMock = new();
        private readonly Mock<IItemRepository> _ItemRepositoryMock = new();
        private readonly ItemProcessor _Sut;

        public ItemProcessorTest()
        {
            _Sut = new ItemProcessor(_CustomerRepositoryMock.Object, _ItemRepositoryMock.Object);
        }

        [Fact]
        public void OnClose_ShouldCallSave_OnRepository()
        {
            //Arrange

            //Act
            _Sut.OnClose();

            //Assert
            _CustomerRepositoryMock.Verify(x => x.SaveChanges(), Times.Once);
        }


        [Fact]
        public void VisitMembership_ShouldAddMembership_WhenAllInputsAreCorrect()
        {
            //Arrange
            var item = new BookClubMembership(Guid.NewGuid(), "Test", 5, 30);
            Guid customerID = Guid.NewGuid();
            _Sut.CustomerId = customerID;
            _ItemRepositoryMock
                .Setup(x => x.GetMembershipById(item.Id))
                .Returns(new MembershipModel() { Id = Guid.NewGuid() });
            _CustomerRepositoryMock
                .Setup(x => x.GetCustomerById(It.IsAny<Guid>()))
                .Returns(new CustomerModel() { Id = Guid.NewGuid() });
            _CustomerRepositoryMock
                .Setup(x => x.DoesCustomerHaveMembership(customerID, It.IsAny<Guid>())).Returns(false);

            //Act
            _Sut.VisitMembership(item);

            //Assert
            _CustomerRepositoryMock.Verify(x => x.AddNewMembership(It.IsAny<CustomerMembershipModel>()), Times.Once);

        }


        [Fact]
        public void VisitMembership_ShouldExecute_WhenAllInputsAreCorrect()
        {
            //Arrange
            var item = new BookClubMembership(Guid.NewGuid(), "Test", 5, 30);
            Guid customerID = Guid.NewGuid();
            _Sut.CustomerId = customerID;
            _ItemRepositoryMock
                .Setup(x => x.GetMembershipById(item.Id))
                .Returns(new MembershipModel() { Id = Guid.NewGuid() });
            _CustomerRepositoryMock
                .Setup(x => x.GetCustomerById(It.IsAny<Guid>()))
                .Returns(new CustomerModel() { Id = Guid.NewGuid() });
            _CustomerRepositoryMock
                .Setup(x => x.DoesCustomerHaveMembership(customerID, It.IsAny<Guid>())).Returns(false);

            //Act and Assert
            _Sut.VisitMembership(item);
        }

        [Fact]
        public void VisitMembership_ShouldError_WhenCustomerAlreadyHaveAMembership()
        {
            //Arrange
            var item = new BookClubMembership(Guid.NewGuid(), "Test", 5, 30);
            Guid customerID = Guid.NewGuid();
            _Sut.CustomerId = customerID;
            _ItemRepositoryMock
                .Setup(x => x.GetMembershipById(item.Id))
                .Returns(new MembershipModel() { Id = Guid.NewGuid() });
            _CustomerRepositoryMock
                .Setup( x=> x.DoesCustomerHaveMembership(customerID, It.IsAny<Guid>())).Returns (true);

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => _Sut.VisitMembership(item));
            Assert.Equal(ex.Message, Message.CustomerAlreadyHaveThisMembership);
        }

        [Fact]
        public void VisitPhysicalProduct_ShouldAppendItem_ToShippingSlip()
        {
            //Arrange
            var item = new Book(Guid.NewGuid(), "Test", 5);

            //Act
            _Sut.VisitPhysicalProduct(item);

            //Assert
            Assert.Equal(_Sut.ShippingSlip.ToString(), $"\"Name\" : {item.Name}, \"Cost\" : {item.Cost} \n");
        }

    }

}
