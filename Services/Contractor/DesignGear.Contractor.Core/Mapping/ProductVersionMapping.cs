using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ProductVersionMapping : Profile
    {
        public ProductVersionMapping()
        {
            CreateMap<ProductVersion, ProductVersionDto>();
            CreateMap<ProductVersionCreateDto, ProductVersion>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()));
            CreateMap<ProductVersionUpdateDto, ProductVersion>();
        }
    }
}
