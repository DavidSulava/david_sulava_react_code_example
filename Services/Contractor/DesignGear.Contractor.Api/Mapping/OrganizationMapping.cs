using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class OrganizationMapping : Profile
    {
        public OrganizationMapping()
        {
            CreateMap<VmOrganizationCreate, OrganizationCreateDto>(MemberList.None);
            CreateMap<OrganizationDto, VmOrganization>(MemberList.None);
        }
    }
}
