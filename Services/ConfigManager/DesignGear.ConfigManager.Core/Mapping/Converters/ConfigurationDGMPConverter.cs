using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping.Converters {
    public class ConfigurationDGMPConverter : ITypeConverter<Configuration, DesignGearModelPackage> {
        public DesignGearModelPackage Convert(Configuration source, DesignGearModelPackage destination, ResolutionContext context) {
            throw new NotImplementedException();
        }
    }
}
