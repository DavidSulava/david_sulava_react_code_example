using AutoMapper;
using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using DesignGear.ModelPackage;

namespace DesignGear.Contractor.Core.Mapping
{
    public class ParameterDefinitionMapping : Profile
    {
        public ParameterDefinitionMapping()
        {
            CreateMap<ParameterDefinition, ParameterDefinitionDto>(MemberList.None);
            CreateMap<DesignGearModelPackage.ParameterRow, ParameterDefinition>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.ValueType, m => m.MapFrom(z => Enum.GetName(typeof(ParameterValueType), z.ValueType)))
                .ForMember(x => x.ParameterId, m => m.MapFrom(z => z.Id))
                .ForMember(x => x.ConfigurationId, m => m.Ignore());
            CreateMap<DesignGearModelPackage.ValueOptionRow, ValueOption>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()));
        }
    }
}
