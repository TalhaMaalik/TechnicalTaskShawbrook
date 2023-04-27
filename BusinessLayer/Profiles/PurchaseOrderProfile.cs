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
            var purchaseOrderMapping = CreateMap<PurchaseOrderCreateDTO,PurchaseOrder>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());
        }
    }
}
