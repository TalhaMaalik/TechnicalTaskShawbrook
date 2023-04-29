using BusinessLayer.Models.Items;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Factory;
using DataAccessLayer.Data;
using DataAccessLayer.DataModel;
using Moq;
using Xunit;

namespace TechnicalTaskShawbrook.Tests.Factory
{
    public class ItemFactoryTest
    {
        private readonly Mock<IItemRepository> _ItemRepositoryMock = new();

        private readonly ItemFactory _Sut;
        public ItemFactoryTest()
        {
            _Sut = new ItemFactory(_ItemRepositoryMock.Object);
        }

        [Fact]
        public void CreateMembership_ShouldReturnBookMembership_WhenMembershipIsBook()
        {
            //Arrange
            var membershipModel = new MembershipModel() { Name = "Test", Days = 30, Type = "BookMembership", Id = Guid.NewGuid()};
            var item = new ItemModel() { Id = Guid.NewGuid() };
            _ItemRepositoryMock.Setup(x => x.GetMembershipByItemId(It.IsAny<Guid>()))
                .Returns(membershipModel);

            //Act
            var membership = _Sut.CreateMembership(item);

            //Assert
            Assert.IsType<BookClubMembership>(membership);
        }
        [Fact]
        public void CreateMembership_ShouldReturnVideoClubMembership_WhenMembershipIsVideo()
        {
            //Arrange
            var membershipModel = new MembershipModel() { Name = "Test", Days = 30, Type = "VideoMembership", Id = Guid.NewGuid() };
            var item = new ItemModel() { Id = Guid.NewGuid() };
            _ItemRepositoryMock.Setup(x => x.GetMembershipByItemId(It.IsAny<Guid>()))
                .Returns(membershipModel);

            //Act
            var membership = _Sut.CreateMembership(item);

            //Assert
            Assert.IsType<VideoClubMembership>(membership);
        }

        [Fact]
        public void CreateMembership_ShouldReturnPremiunMembership_WhenMembershipIsPremium()
        {
            //Arrange
            var membershipModel = new MembershipModel() { Name = "Test", Days = 30, Type = "PremiumMembership", Id = Guid.NewGuid() };
            var item = new ItemModel() { Id = Guid.NewGuid() };
            _ItemRepositoryMock.Setup(x => x.GetMembershipByItemId(It.IsAny<Guid>()))
                .Returns(membershipModel);

            //Act
            var membership = _Sut.CreateMembership(item);

            //Assert
            Assert.IsType<PremiumClubMembership>(membership);
        }

        [Fact]
        public void CreateMembership_ShouldReturnUnknownMembership_WhenMembershipIsUnknown()
        {
            //Arrange
            var membershipModel = new MembershipModel() { Name = "Test", Days = 30, Type = "unknown", Id = Guid.NewGuid() };
            var item = new ItemModel() { Id = Guid.NewGuid() };
            _ItemRepositoryMock.Setup(x => x.GetMembershipByItemId(It.IsAny<Guid>()))
                .Returns(membershipModel);

            //Act
            var membership = _Sut.CreateMembership(item);

            //Assert
            Assert.IsType<UnknownMembership>(membership);
        }

        [Fact]
        public void Create_ShouldReturnMembership_WhenItemIsMembership()
        {
            //Arrange
            var item = new ItemModel() { Id = Guid.NewGuid(), Type = "Membership" };
            var guid = Guid.NewGuid();
            var membershipModel = new MembershipModel() { Name = "Test", Days = 30, Type = "BookMembership", Id = Guid.NewGuid() };
            _ItemRepositoryMock.Setup(x => x.GetItemById(guid)).Returns(item);
            _ItemRepositoryMock.Setup(x => x.GetMembershipByItemId(It.IsAny<Guid>()))
                .Returns(membershipModel);

            //Act
            var result = _Sut.Create(guid);

            //Assert
            Assert.IsAssignableFrom<Membership>(result);
        }

        [Fact]
        public void Create_ShouldReturnVideo_WhenItemIsVideo()
        {
            //Arrange
            var item = new ItemModel() { Id = Guid.NewGuid(), Type = "Video" };
            var guid = Guid.NewGuid();
            _ItemRepositoryMock.Setup(x => x.GetItemById(guid)).Returns(item);

            //Act
            var result = _Sut.Create(guid);

            //Assert
            Assert.IsType<Video>(result);
        }

        [Fact]
        public void Create_ShouldReturnBook_WhenItemIsBook()
        {
            //Arrange
            var item = new ItemModel() { Id = Guid.NewGuid(), Type = "Book" };
            var guid = Guid.NewGuid();
            _ItemRepositoryMock.Setup(x => x.GetItemById(guid)).Returns(item);

            //Act
            var result = _Sut.Create(guid);

            //Assert
            Assert.IsType<Book>(result);
        }

        [Fact]
        public void Create_ShouldReturnUnknown_WhenItemIsUnknown()
        {
            //Arrange
            var item = new ItemModel() { Id = Guid.NewGuid(), Type = "abc" };
            var guid = Guid.NewGuid();
            _ItemRepositoryMock.Setup(x => x.GetItemById(guid)).Returns(item);

            //Act
            var result = _Sut.Create(guid);

            //Assert
            Assert.IsType<UnknownItem>(result);
        }

        [Fact]
        public void Create_ShouldReturnItem_WithCorrectValues()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var item = new ItemModel() { Id = Guid.NewGuid(), Name = "Test", Cost = 5 };
            _ItemRepositoryMock.Setup(x => x.GetItemById(It.IsAny<Guid>())).Returns(item);

            //Act
            var result = _Sut.Create(It.IsAny<Guid>());

            //Assert
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.Id, result.Id);
            Assert.Equal(item.Cost, result.Cost);

        }

        [Fact]
        public void CreateMembership_ShouldReturnMembership_WithCorrectValues()
        {
            //Arrange
            var membershipModel = new MembershipModel() { Name = "Test", Days = 30, Type = "VideoMembership", Id = Guid.NewGuid() };
            var item = new ItemModel() { Id = Guid.NewGuid() };
            _ItemRepositoryMock.Setup(x => x.GetMembershipByItemId(It.IsAny<Guid>()))
                .Returns(membershipModel);

            //Act
            var membership = _Sut.CreateMembership(item);

            //Assert
            Assert.Equal(membershipModel.Name, membership.Name);
            Assert.Equal(membershipModel.Days, membership.Days);
            Assert.Equal(membershipModel.Id, membership.Id);
        }

    }
}
