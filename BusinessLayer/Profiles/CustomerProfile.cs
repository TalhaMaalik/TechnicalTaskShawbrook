using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.DataModel;

namespace BusinessLayer.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerCreateDTO, CustomerModel>();
            CreateMap<CustomerModel, CustomerReadDTO>();
        }
    }
}
