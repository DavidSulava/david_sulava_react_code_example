using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>(MemberList.None);
            CreateMap<Product, ProductItemDto>(MemberList.None)
                .ForMember(x => x.CurrentVersion, m => m.MapFrom(z => z.CurrentProductVersion != null ? z.CurrentProductVersion.Name : string.Empty));
            CreateMap<ProductCreateDto, Product>(MemberList.None);
            CreateMap<ProductUpdateDto, Product>(MemberList.None);
        }
    }
}
