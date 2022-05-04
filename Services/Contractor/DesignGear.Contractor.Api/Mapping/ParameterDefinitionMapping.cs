﻿using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models;

namespace DesignGear.Contractor.Api.Mapping
{
    public class ParameterDefinitionMapping : Profile
    {
        public ParameterDefinitionMapping()
        {
            CreateMap<ParameterDefinitionDto, VmParameterDefinition>(MemberList.None);
        }
    }
}