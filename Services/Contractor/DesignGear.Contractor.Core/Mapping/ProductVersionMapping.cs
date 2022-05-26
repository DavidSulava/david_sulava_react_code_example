using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ProductVersionMapping : Profile
    {
        public ProductVersionMapping()
        {
            CreateMap<ProductVersion, ProductVersionDto>(MemberList.None)
                .ForMember(x => x.IsCurrent, m => m.MapFrom(z => z.Product.CurrentVersionId == z.Id));
            CreateMap<ProductVersion, ProductVersionItemDto>(MemberList.None);
            CreateMap<ProductVersionCreateDto, ProductVersion>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()));
            CreateMap<ProductVersionUpdateDto, ProductVersion>(MemberList.None);
            // CreateMap<ProductVersionCreateDto, CreateConfigurationRequest>(MemberList.None);
            CreateMap<ProductVersionCreateDto, VmConfigurationCreate>(MemberList.None)
                .ForMember(x => x.ConfigurationPackage, m => m.MapFrom(z => z.ModelFile));
        }
    }
}
