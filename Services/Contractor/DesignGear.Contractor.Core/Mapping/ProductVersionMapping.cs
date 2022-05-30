using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;
using Microsoft.AspNetCore.Http;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ProductVersionMapping : Profile
    {
        public ProductVersionMapping()
        {
            CreateMap<IFormFile, ProductVersionPreview>(MemberList.None)
                .ForMember(x => x.FileName, m => m.MapFrom(x => x.FileName))
                .ForMember(x => x.Content, m => m.MapFrom(x => x.OpenReadStream().ToByteArray()));

            CreateMap<ProductVersion, ProductVersionDto>(MemberList.None)
                .ForMember(x => x.IsCurrent, m => m.MapFrom(z => z.Product.CurrentVersionId == z.Id));
            CreateMap<ProductVersion, ProductVersionItemDto>(MemberList.None);
            CreateMap<ProductVersionCreateDto, ProductVersion>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.ProductVersionPreviews, m => {
                    m.Condition(x => x.ImageFiles != null && x.ImageFiles.Count > 0);
                    m.MapFrom(x => x.ImageFiles); 
                });
            CreateMap<ProductVersionUpdateDto, ProductVersion>(MemberList.None)
                .ForMember(x => x.ProductVersionPreviews, m => m.MapFrom(x => x.ImageFiles)); ;
            // CreateMap<ProductVersionCreateDto, CreateConfigurationRequest>(MemberList.None);
            CreateMap<ProductVersionCreateDto, VmConfigurationCreate>(MemberList.None)
                .ForMember(x => x.ConfigurationPackage, m => m.MapFrom(z => z.ModelFile));
        }
    }
}
