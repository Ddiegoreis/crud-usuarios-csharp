using CrudUsuarios.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudUsuarios.Services
{
    public class TokenService
    {
        public Claim[] buildClaims(IConfiguration configuration, User user)
        {
            var claim = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Nome", user.Nome),
                new Claim("Email", user.Email)
            };

            return claim;
        }

        public SymmetricSecurityKey returnSymetricKey(IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        
            return key;
        }

        public SigningCredentials returnSignCredential(SymmetricSecurityKey key)
        {
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return credential;
        }

        public JwtSecurityToken returnJwtToken(
            IConfiguration configuration,
            Claim[] claims,
            SigningCredentials signIn
        ) {
            var jwtToken = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn
                );

            return jwtToken;
        }

        public string returnJwtTokenResponse(JwtSecurityToken token)
        {
            var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenResponse;
        }
    }
}
