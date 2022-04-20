using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IUserService
    {
        User? GetById(Guid userId);

        Guid CreateUser(UserCreateDto user);

        bool VerifyEmail(string email);
    }
}
