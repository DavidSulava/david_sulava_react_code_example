using DesignGear.Contractor.Core.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        User? GetById(Guid userId);
    }
}
