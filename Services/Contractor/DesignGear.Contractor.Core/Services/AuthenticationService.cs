using Microsoft.Extensions.Options;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using DesignGear.Common.Extensions;
using AutoMapper;
using DesignGear.Common.Exceptions;
using DesignGear.Contractor.Core.Data.Entity;

namespace DesignGear.Contractor.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataAccessor _dataAccessor;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;

        public AuthenticationService(DataAccessor dataAccessor, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _dataAccessor = dataAccessor;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model)
        {
            //todo Anton это можно сделать в конце фазы. Необходимо будет хранить пароли в бд хэшированными, вместе с солью
            var user = await _dataAccessor.Reader.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var result = user.MapTo<AuthenticateResponseDto>(_mapper);
            result.Token = JwtHelper.generateJwtToken(user, null, _appSettings.Secret);

            return result;
        }

        public async Task<AuthenticateResponseDto> SetOrganizationAsync(Guid userId, Guid organizationId)
        {

            var user = await _dataAccessor.Reader.Users.FirstOrDefaultAsync(x => x.Id == userId);
            

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var result = user.MapTo<AuthenticateResponseDto>(_mapper);
            result.Token = JwtHelper.generateJwtToken(user, organizationId, _appSettings.Secret);

            return result;
        }

        public async Task SendPasswordRecoveryRequestAsync(PasswordRecoveryRequestDto request) {
            var user = await _dataAccessor.Editor.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null) {
                throw new EntityNotFoundException<User>(request.Email);
            }

            user.PasswordRecoveryKey = Guid.NewGuid();
            user.PasswordRecoveryKeyCreated = DateTimeOffset.Now;

            await _dataAccessor.Editor.SaveAsync();

            //todo Send email 
        }

        public async Task ChangePasswordAsync(PasswordRecoveryCommitDto commit) {
            if (!string.Equals(commit.NewPassword, commit.ConfirmPassword)) {
                throw new InvalidDataException("Passwords are not equal");
            }

            var user = await _dataAccessor.Editor.Users
                .Where(x => x.Email == commit.Email 
                    && x.PasswordRecoveryKey == commit.PasswordRecoveryKey 
                    && x.PasswordRecoveryKeyCreated <= DateTimeOffset.Now.AddMinutes(15))
                .FirstOrDefaultAsync();
            if (user == null) {
                throw new EntityNotFoundException<User>("User is not founded by email or invalid recovery key or recovery key is expired");
            }

            user.Password = commit.NewPassword;
            user.PasswordChanged = DateTimeOffset.Now;

            await _dataAccessor.Editor.SaveAsync();
        }
    }
}
