using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model);

        Task<AuthenticateResponseDto> SetOrganizationAsync(Guid organizationId);
    }
}
