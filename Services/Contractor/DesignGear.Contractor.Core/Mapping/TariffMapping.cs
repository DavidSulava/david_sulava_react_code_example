using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Mapping
{
    public class TariffMapping : Profile
    {
        public TariffMapping()
        {
            CreateMap<Tariff, TariffDto>(MemberList.None);
        }
    }
}
