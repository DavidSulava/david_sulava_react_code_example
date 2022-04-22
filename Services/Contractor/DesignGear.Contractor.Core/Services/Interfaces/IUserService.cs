using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(Guid id);

        Task<Guid> CreateUserAsync(UserCreateDto user);

        Task<bool> VerifyEmailAsync(string email);
    }
}
