using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.ConfigManager.Api.Mapping
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<VmConfigurationCreate, ConfigurationCreateDto>(MemberList.None);
            CreateMap<VmParameterValue, ParameterValueDto>(MemberList.None);
            CreateMap<VmConfigurationRequest, ConfigurationRequestDto>(MemberList.None);
            CreateMap<ConfigurationItemDto, VmConfigurationItem>(MemberList.None);
            /*CreateMap<VmConfigurationUpdate, ConfigurationUpdateDto>(MemberList.None);
            CreateMap<ConfigurationDto, VmConfiguration>(MemberList.None);
            CreateMap<ConfigurationItemDto, VmConfigurationItem>(MemberList.None);*/
        }
    }
}
