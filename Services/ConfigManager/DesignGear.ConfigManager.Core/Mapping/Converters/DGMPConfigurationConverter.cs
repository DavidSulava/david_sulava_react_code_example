using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ModelPackage;

namespace DesignGear.ConfigManager.Core.Mapping.Converters
{
    public class DGMPConfigurationConverter : ITypeConverter<DesignGearModelPackage, ICollection<Configuration>>
    {
        public ICollection<Configuration> Convert(DesignGearModelPackage source, ICollection<Configuration> destination, ResolutionContext context)
        {
            var configurations = context.Mapper.Map<ICollection<Configuration>>(source.Configuration.Rows);

            foreach (var config in configurations)
            {
                config.ComponentDefinition = context.Mapper.Map<ComponentDefinition>(source.ComponentDefinition.FirstOrDefault(x => x.Id == config.ComponentDefinitionIdInternal));
                config.ConvigurationInstances = context.Mapper.Map<ICollection<ConfigurationInstance>>(source.ConfigurationInstance.Where(x => x.ConfigurationId == config.ConfigurationId));
                config.FileItems = context.Mapper.Map<ICollection<FileItem>>(source.File.Where(x => x.ConfigurationId == config.ConfigurationId));
                //config.TargetFileId = config.FileItems.First(x => x.FileId == config.TargetFileIdInternal).Id;
                //config.TargetFileItem = config.FileItems.First(x => x.FileId == config.TargetFileIdInternal);
                config.ParameterDefinitions = context.Mapper.Map<ICollection<ParameterDefinition>>(source.Parameter.Where(x => x.ConfigurationId == config.ConfigurationId));
                foreach (var param in config.ParameterDefinitions)
                {
                    param.ValueOptions = context.Mapper.Map<ICollection<ValueOption>>(source.ValueOption.Where(x => x.ParameterId == param.ParameterId));
                }
            }
            foreach (var config in configurations)
            {
                config.ComponentDefinition.TemplateConfigurationId =
                    configurations.FirstOrDefault(x => x.ConfigurationId == config.ComponentDefinition.TemplateConfigurationIdInternal).Id;
            }

            return configurations;
        }
    }
}
