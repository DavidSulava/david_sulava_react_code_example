using AutoMapper;
using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;

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

        public User? GetById(Guid userId)
        {
            return _dataAccessor.Reader.Users.FirstOrDefault(x => x.Id == userId);
        }

        public Guid CreateUser(UserCreateDto user)
        {
            if (VerifyEmail(user.Email))
                return Guid.Empty;

            var newUser = _mapper.Map<User>(user);
            _dataAccessor.Editor.Create(newUser);
            _dataAccessor.Editor.Save();
            return newUser.Id;
        }

        public bool VerifyEmail(string email)
        {
            return _dataAccessor.Reader.Users.Any(x => x.Email == email);
        }
    }
}
