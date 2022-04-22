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
            CreateMap<ProductVersionCreateDto, ProductVersion>();
            CreateMap<ProductVersionUpdateDto, ProductVersion>();
        }
    }
}
