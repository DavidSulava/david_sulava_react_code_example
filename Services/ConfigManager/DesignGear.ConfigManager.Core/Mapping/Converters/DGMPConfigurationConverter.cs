using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping.Converters {
    public class DGMPConfigurationConverter : ITypeConverter<DesignGearModelPackage, Configuration> {
        public Configuration Convert(DesignGearModelPackage source, Configuration destination, ResolutionContext context) {
            throw new NotImplementedException();
        }
    }
}
