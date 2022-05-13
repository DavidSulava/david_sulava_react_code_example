using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<VmProductCreate, ProductCreateDto>(MemberList.None);
            CreateMap<VmProductUpdate, ProductUpdateDto>(MemberList.None);
            CreateMap<ProductDto, VmProduct>(MemberList.None);
        }
    }
}
