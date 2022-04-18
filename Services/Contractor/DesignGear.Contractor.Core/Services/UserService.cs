using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services
{
    public class UserService : IUserService
    {
        private readonly DataAccessor _dataAccessor;

        public UserService(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public Guid CreateUser(UserCreateDto user)
        {
            if (VerifyEmail(user.Email))
                return Guid.Empty;

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Created = DateTime.Now,
                Role = UserRole.User
            };
            _dataAccessor.Editor.Create(newUser);
            _dataAccessor.Editor.Save();
            return newUser.UserId;
        }

        public bool VerifyEmail(string email)
        {
            return _dataAccessor.Reader.Users.Any(x => x.Email == email);
        }
    }
}
