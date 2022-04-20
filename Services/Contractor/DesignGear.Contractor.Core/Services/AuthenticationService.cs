using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Dto;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataAccessor _dataAccessor;
        private readonly AppSettings _appSettings;

        public AuthenticationService(DataAccessor dataAccessor, IOptions<AppSettings> appSettings)
        {
            _dataAccessor = dataAccessor;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestModel model)
        {
            //todo Anton это можно сделать в конце фазы. Необходимо будет хранить пароли в бд хэшированными, вместе с солью
            var user = _dataAccessor.Reader.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponseDto(user, token);
        }

        //todo Anton лучше сразу вынести в отдельный статический класс, например JwtHelper
        // helper methods
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
