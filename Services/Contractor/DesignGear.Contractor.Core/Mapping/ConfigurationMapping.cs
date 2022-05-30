using AutoMapper;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<VmComponentParameterDefinitions, ConfigurationParametersDto>(MemberList.None);
            CreateMap<VmParameterDefinition, ParameterDefinitionDto>(MemberList.None);
            CreateMap<VmValueOption, ValueOptionDto>(MemberList.None);
            CreateMap<ConfigurationParametersDto, VmComponentParameterDefinitions>(MemberList.None);
            CreateMap<ParameterDefinitionDto, VmParameterDefinition>(MemberList.None);
            CreateMap<ValueOptionDto, VmValueOption>(MemberList.None);
        }
    }
}
