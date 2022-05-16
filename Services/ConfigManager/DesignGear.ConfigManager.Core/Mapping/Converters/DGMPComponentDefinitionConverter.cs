using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping.Converters {
    public class DGMPComponentDefinitionConverter : ITypeConverter<DesignGearModelPackage, ComponentDefinition> {
        public ComponentDefinition Convert(DesignGearModelPackage source, ComponentDefinition destination, ResolutionContext context) {
            throw new NotImplementedException();
        }
    }
}
