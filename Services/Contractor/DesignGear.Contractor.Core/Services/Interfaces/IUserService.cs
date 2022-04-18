using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IUserService
    {
        Guid CreateUser(UserCreateDto user);
    }
}
