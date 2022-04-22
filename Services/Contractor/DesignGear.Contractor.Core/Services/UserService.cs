using AutoMapper;
using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public UserService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _dataAccessor.Reader.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<Guid> CreateUserAsync(UserCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            if (await VerifyEmailAsync(create.Email))
                return Guid.Empty;

            var newUser = _mapper.Map<User>(create);
            _dataAccessor.Editor.Create(newUser);
            await _dataAccessor.Editor.SaveAsync();
            return newUser.Id;
        }

        public async Task<bool> VerifyEmailAsync(string email)
        {
            return await _dataAccessor.Reader.Users.AnyAsync(x => x.Email == email);
        }
    }
}
