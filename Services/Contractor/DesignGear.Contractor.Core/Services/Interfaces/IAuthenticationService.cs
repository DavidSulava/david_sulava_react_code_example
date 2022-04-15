using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        User? GetById(Guid userId);
    }
}
