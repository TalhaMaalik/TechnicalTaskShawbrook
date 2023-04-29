using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Models.Base;
using BusinessLayer.Profiles;
using DataAccessLayer.DataModel;
using Xunit;

namespace TechnicalTaskShawbrook.Tests
{
    public class PurchaseOrderProfileTest
    {
        private readonly IMapper _Mapper;

        public PurchaseOrderProfileTest()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PurchaseOrderProfile>();
            });

            _Mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void CreateMap_ShouldMapPurchaseOrderCreateDTO_ToPurchaseOrder()
        {

            //Act
            var result = _Mapper.Map<PurchaseOrder>(new PurchaseOrderCreateDTO());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateMap_ShouldMapAllValuesPurchaseOrderCreateDTO_ToPurchaseOrder()
        {
            //Arrange
            var id = Guid.NewGuid();
            var purchaseOrderDTO = new PurchaseOrderCreateDTO() { CustomerId = id };

            //Act
            var result = _Mapper.Map<PurchaseOrder>(purchaseOrderDTO);

            //Assert
            Assert.Equal(purchaseOrderDTO.CustomerId, result.CustomerId);
        }

        [Fact]
        public void CreateMap_ShouldNotMapPurchaseOrderCreateDTO_ToPurchaseOrder_WhenValueIsNull()
        {

            //Act
            var result = _Mapper.Map<PurchaseOrder>(null);

            //Assert
            Assert.Null(result);
        }
    }
}
