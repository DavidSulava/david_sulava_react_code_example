﻿using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Mapping
{
    public class OrganizationMapping : Profile
    {
        public OrganizationMapping()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<OrganizationDto, Organization>();
            CreateMap<OrganizationDto, UserAssignment>();
        }
    }
}