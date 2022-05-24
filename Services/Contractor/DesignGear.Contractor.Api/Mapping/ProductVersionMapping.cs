using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class ProductVersionMapping : Profile
    {
        public ProductVersionMapping()
        {
            CreateMap<VmProductVersionCreate, ProductVersionCreateDto>(MemberList.None);
            CreateMap<VmProductVersionUpdate, ProductVersionUpdateDto>(MemberList.None);
            CreateMap<ProductVersionDto, VmProductVersion>(MemberList.None);
            CreateMap<ProductVersionItemDto, VmProductVersionItem>(MemberList.None);
        }
    }
}
