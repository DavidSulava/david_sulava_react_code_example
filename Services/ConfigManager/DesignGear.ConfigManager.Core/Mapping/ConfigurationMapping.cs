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
            CreateMap<ConfigurationRequestDto, Configuration>(MemberList.None)
                .ForMember(x => x.Status, m => m.MapFrom(x => ConfigurationStatus.InQueue))
                .ForMember(x => x.SvfStatus, m => m.MapFrom(x => ConfigurationStatus.InQueue));
            CreateMap<ConfigurationCreateDto, Configuration>(MemberList.None)
                .ForMember(x => x.Status, m => m.MapFrom(x => ConfigurationStatus.Ready))
                .ForMember(x => x.SvfStatus, m => m.MapFrom(x => ConfigurationStatus.InQueue));
            CreateMap<ConfigurationUpdateDto, Configuration>(MemberList.None);
        }
    }
}
