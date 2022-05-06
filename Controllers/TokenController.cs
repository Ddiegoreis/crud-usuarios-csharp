using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CrudUsuarios.Models;
using CrudUsuarios.Utils;

namespace CrudUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        public IConfiguration configuration;
        
        private readonly ApiContext _context;

        public TokenController(IConfiguration configuration, ApiContext context)
        {
            this.configuration = configuration;

            this._context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> AutenticaUsuario(User userAuth)
        {
            if (userAuth == null && userAuth.Email == null && userAuth.Senha == null)
            {
                return BadRequest("Usuário inválido");
            }

            var user = await GetUsuario(userAuth.Email, userAuth.Senha);

            if (user == null)
            {
                return BadRequest("Credenciais inválidas");
            }
                
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, this.configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Nome", user.Nome),
                new Claim("Email", user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));

            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = 
                new JwtSecurityToken(this.configuration["Jwt:Issuer"],
                    this.configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn
                );

            string tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenHandler);    
        }

        private async Task<User> GetUsuario(string email, string password)
        {
            return await _context.User.FirstOrDefaultAsync(
                user => 
                    user.Email == email && user.Senha == EncryptPassword.execute(password)
            );
        }
    }
}