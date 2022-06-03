using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping
{
    public class AuthenticateMapping : Profile
    {
        public AuthenticateMapping()
        {
            CreateMap<VmAuthenticateRequest, AuthenticateRequestDto>(MemberList.None);
            CreateMap<AuthenticateResponseDto, VmAuthenticateResponse>(MemberList.None);
            CreateMap<VmPasswordRecoveryRequest, PasswordRecoveryRequestDto>(MemberList.None);
            CreateMap<VmPasswordRecoveryCommit, PasswordRecoveryCommitDto>(MemberList.None);
        }
    }
}
