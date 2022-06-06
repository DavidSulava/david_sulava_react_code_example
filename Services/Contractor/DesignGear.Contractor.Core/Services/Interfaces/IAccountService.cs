using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces {
    public interface IAccountService {
        Task<AccountDto> GetAccountAsync();
        Task UpdateAccountAsync(AccountUpdateDto update);
    }
}