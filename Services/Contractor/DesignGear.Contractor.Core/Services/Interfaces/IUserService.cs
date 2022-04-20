using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetById(Guid userId);

        Task<Guid> CreateUser(UserCreateDto user);

        Task<bool> VerifyEmail(string email);
    }
}
