using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models;

namespace DesignGear.Contractor.Api.Mapping
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<VmConfigurationCreate, ConfigurationCreateDto>(MemberList.None);
            CreateMap<VmConfigurationUpdate, ConfigurationUpdateDto>(MemberList.None);
            CreateMap<ConfigurationDto, VmConfiguration>(MemberList.None);
        }
    }
}
