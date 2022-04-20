using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models;

namespace DesignGear.Contractor.Api.Mapping
{
    public class AuthenticateMapping : Profile
    {
        public AuthenticateMapping()
        {
            CreateMap<VmAuthenticateRequest, AuthenticateRequestDto>(MemberList.None);
            CreateMap<AuthenticateResponseDto, VmAuthenticateResponse>(MemberList.None);
        }
    }
}
