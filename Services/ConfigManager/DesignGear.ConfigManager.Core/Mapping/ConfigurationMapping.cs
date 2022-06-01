using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping {
    public class ConfigurationMapping : Profile {
        public ConfigurationMapping() {
            CreateMap<Configuration, ConfigurationItemDto>(MemberList.None)
                .ForMember(x => x.ConfigurationName, m => m.MapFrom(x => x.Name))
                .ForMember(x => x.ComponentName, m => m.MapFrom(x => x.ComponentDefinition.Name));
            CreateMap<Configuration, ConfigurationItemExDto>(MemberList.None)
                .ForMember(x => x.ConfigurationName, m => m.MapFrom(x => x.Name))
                .ForMember(x => x.ComponentName, m => m.MapFrom(x => x.ComponentDefinition.Name))
                .ForMember(x => x.ProductVersionId, m => m.MapFrom(x => x.ComponentDefinition.ProductVersionId))
                .ForMember(x => x.RootFileName, m => m.MapFrom(x => x.TargetFileItem.FilePath));
            CreateMap<ConfigurationRequestDto, Configuration>(MemberList.None)
                .ForMember(x => x.UniqueId, m => m.MapFrom(z => Guid.Empty))
                .ForMember(x => x.ErrorMessage, m => m.MapFrom(z => string.Empty))
                .ForMember(x => x.TemplateConfigurationId, m => m.MapFrom(x => x.BaseConfigurationId))
                .ForMember(x => x.Status, m => m.MapFrom(x => ConfigurationStatus.InQueue))
                .ForMember(x => x.SvfStatus, m => m.MapFrom(x => ConfigurationStatus.InQueue));
            CreateMap<ConfigurationCreateDto, Configuration>(MemberList.None)
                .ForMember(x => x.Status, m => m.MapFrom(x => ConfigurationStatus.Ready))
                .ForMember(x => x.SvfStatus, m => m.MapFrom(x => ConfigurationStatus.InQueue));
            CreateMap<ConfigurationCreateDto, ComponentDefinition>(MemberList.None);
            CreateMap<ConfigurationUpdateDto, Configuration>(MemberList.None);
            CreateMap<ConfigurationUpdateSvfDto, Configuration>(MemberList.None);
        }
    }
}
