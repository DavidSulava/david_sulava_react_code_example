﻿using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class TariffMapping : Profile
    {
        public TariffMapping()
        {
            CreateMap<TariffDto, VmTariff>(MemberList.None);
        }
    }
}