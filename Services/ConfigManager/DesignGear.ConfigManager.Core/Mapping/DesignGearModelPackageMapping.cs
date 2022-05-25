using AutoMapper;
using DesignGear.Common.Enums;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ConfigManager.Core.Mapping.Converters;
using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping
{
    public class DesignGearModelPackageMapping : Profile
    {
        public DesignGearModelPackageMapping()
        {
            CreateMap<DesignGearModelPackage, ICollection<Configuration>>(MemberList.None).ConvertUsing(new DGMPConfigurationConverter());

            CreateMap<DesignGearModelPackage.ParameterRow, ParameterDefinition>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.ValueType, m => m.MapFrom(z => Enum.GetName(typeof(ParameterValueType), z.ValueType)))
                .ForMember(x => x.ParameterId, m => m.MapFrom(z => z.Id))
                .ForMember(x => x.ConfigurationId, m => m.Ignore());

            CreateMap<DesignGearModelPackage.ValueOptionRow, ValueOption>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()));

            CreateMap<DesignGearModelPackage.ConfigurationRow, Configuration>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.ConfigurationId, m => m.MapFrom(z => z.Id))
                .ForMember(x => x.TargetFileIdInternal, m => m.MapFrom(z => z.TargetFileId))
                .ForMember(x => x.TargetFileId, m => m.Ignore())
                .ForMember(x => x.ComponentDefinitionIdInternal, m => m.MapFrom(z => z.ComponentDefinitionId))
                .ForMember(x => x.ComponentDefinitionId, m => m.Ignore())
                .ForMember(x => x.ErrorMessage, m => m.MapFrom(z => String.Empty));

            CreateMap<DesignGearModelPackage.ComponentDefinitionRow, ComponentDefinition>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.TemplateConfigurationId, m => m.Ignore())
                .ForMember(x => x.TemplateConfigurationIdInternal, m => m.MapFrom(z => z.TemplateConfigurationId));

            CreateMap<DesignGearModelPackage.ConfigurationInstanceRow, ConfigurationInstance>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()));

            CreateMap<DesignGearModelPackage.FileRow, FileItem>(MemberList.None)
                .ForMember(x => x.Id, m => m.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.FileId, m => m.MapFrom(z => z.Id))
                .ForMember(x => x.ConfigurationId, m => m.Ignore());



            CreateMap<ICollection<Configuration>, DesignGearModelPackage>(MemberList.None);
        }
    }
}
