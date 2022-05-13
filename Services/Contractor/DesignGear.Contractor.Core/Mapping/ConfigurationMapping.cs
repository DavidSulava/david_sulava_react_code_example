using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using DesignGear.ModelPackage;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Configuration, ConfigurationDto>(MemberList.None);
           // CreateMap<ConfigurationCreateDto, Configuration>(MemberList.None);
            CreateMap<ConfigurationUpdateDto, Configuration>(MemberList.None);
            CreateMap<DesignGearModelPackage.ConfigurationRow, Configuration>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.TargetFileId, m => m.Ignore());
        }
    }
}
