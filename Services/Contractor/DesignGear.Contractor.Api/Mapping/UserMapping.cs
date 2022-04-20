using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models;

namespace DesignGear.Contractor.Api.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<VmUserCreate, UserCreateDto>(MemberList.None);
        }
    }
}

