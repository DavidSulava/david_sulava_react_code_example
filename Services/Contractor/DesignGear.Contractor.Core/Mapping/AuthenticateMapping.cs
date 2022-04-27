using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Mapping
{
    public class AuthenticateMapping : Profile
    {
        public AuthenticateMapping()
        {
            CreateMap<User, AuthenticateResponseDto>(MemberList.None);
        }
    }
}
