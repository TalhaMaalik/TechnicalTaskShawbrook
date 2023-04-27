using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Models.Base;
using DataAccessLayer.DataModel;

namespace BusinessLayer.Profiles
{
    public class PurchaseOrderProfile : Profile
    {
        public PurchaseOrderProfile() 
        {
            var purchaseOrderMapping = CreateMap<PurchaseOrderCreateDTO,PurchaseOrder>();
            purchaseOrderMapping.ForAllMembers(opt => opt.Ignore());
            purchaseOrderMapping.ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        }
    }
}
