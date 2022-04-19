using DesignGear.Contractor.Core.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestModel model);
    }
}
