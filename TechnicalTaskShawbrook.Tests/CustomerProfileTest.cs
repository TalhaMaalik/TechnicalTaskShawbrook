using AutoMapper;
using BusinessLayer.Controllers;
using BusinessLayer.DTOs;
using BusinessLayer.Profiles;
using DataAccessLayer.DataModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TechnicalTaskShawbrook.Tests
{
    public class CustomerProfileTest
    {
        private readonly IMapper _Mapper;

        public CustomerProfileTest()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerProfile>();
            });

            _Mapper = mapperConfig.CreateMapper();
        }


        [Fact]
        public void CreateMap_ShouldMapCustomerModel_ToCustomerReadDTO()
        {

            //Act
            var result = _Mapper.Map<CustomerReadDTO>(new CustomerModel());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateMap_ShouldMapAllValuesCustomerModel_ToCustomerReadDTO()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var customerModel = new CustomerModel() { Id = guid, Name = "Talha Malik", Email = "talha@test.com" };

            //Act
            var result = _Mapper.Map<CustomerReadDTO>(customerModel);

            //Assert
            Assert.Equal(customerModel.Id, result.Id);
            Assert.Equal(customerModel.Name, result.Name);
            Assert.Equal(customerModel.Email, result.Email);
        }

        [Fact]
        public void CreateMap_ShouldNotMapCustomerModel_ToCustomerReadDTO_WhenPassedNull()
        {

            //Act
            var result = _Mapper.Map<CustomerReadDTO>(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateMap_ShouldMapCustomerCreateDTO_ToCustomerModel()
        {

            //Act
            var result = _Mapper.Map<CustomerModel>(new CustomerCreateDTO());

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void CreateMap_ShouldMapAllValuesCustomerCreateDTO_ToCustomerModel()
        {
            //Arrange
            var customerDTO = new CustomerCreateDTO() { Name = "Talha Malik", Email = "talha@test.com" };

            //Act
            var result = _Mapper.Map<CustomerModel>(customerDTO);

            //Assert
            Assert.Equal(customerDTO.Name, result.Name);
            Assert.Equal(customerDTO.Email, result.Email);
        }
        [Fact]
        public void CreateMap_ShouldNotMapCustomerCreateDTO_ToCustomerModel_WhenPassedNull()
        {

            //Act
            var result = _Mapper.Map<CustomerModel>(null);

            // Assert
            Assert.Null(result);
        }
    }
}
