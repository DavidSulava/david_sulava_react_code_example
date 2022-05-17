using AutoMapper;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.Contracts.Dto.ConfigManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Mapping {
    public class ParameterMapping : Profile {
        public ParameterMapping() {
            //CreateMap<ParameterValueDto, ParameterValue>(MemberList.None);
        }
    }
}
