using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesignGear.Contractor.Core.Helpers
{
    public static class JwtHelper
    {
        public static string generateJwtToken(User user, Guid? organizationId, string secretPhrase)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretPhrase);
            var claims = new List<Claim>();
            claims.Add(new Claim("UserId", user.Id.ToString()));
            if (organizationId != null)
            {
                claims.Add(new Claim("OrganizationId", organizationId.ToString()));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
