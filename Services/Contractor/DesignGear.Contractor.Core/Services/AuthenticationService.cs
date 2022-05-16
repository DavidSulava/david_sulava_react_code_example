using Microsoft.Extensions.Options;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using DesignGear.Common.Extensions;
using AutoMapper;

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
            result.Token = JwtHelper.generateJwtToken(user, _appSettings.Secret);

            return result;
        }

        public async Task<AuthenticateResponseDto> SetOrganizationAsync(Guid organizationId)
        {

            var user = await _dataAccessor.Reader.Users.FirstOrDefaultAsync();// x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var result = user.MapTo<AuthenticateResponseDto>(_mapper);
            result.Token = JwtHelper.generateJwtToken(user, _appSettings.Secret);

            return result;
        }
    }
}
