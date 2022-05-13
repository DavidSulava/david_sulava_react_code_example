using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.Contracts.Dto.ConfigManager;
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
        }
    }
}
