using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping {
    public class DesignGearModelPackageMapping : Profile {
        public DesignGearModelPackageMapping() {
            CreateMap<DesignGearModelPackage, ComponentDefinition>(MemberList.None);
            CreateMap<DesignGearModelPackage, Configuration>(MemberList.None);
            CreateMap<Configuration, DesignGearModelPackage>(MemberList.None);
        }
    }
}
