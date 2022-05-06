using CrudUsuarios.Models;
using CrudUsuarios.Services;
using CrudUsuarios.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CrudUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        public IConfiguration configuration;
        
        private readonly ApiContext _context;
        private readonly TokenService _service;
        private string buildTokenResponse(User user)
        {
            var claims = _service.buildClaims(configuration, user);

            SymmetricSecurityKey key = _service.returnSymetricKey(configuration);

            SigningCredentials signIn = _service.returnSignCredential(key);

            JwtSecurityToken token = _service.returnJwtToken(configuration, claims, signIn);

            string tokenHandler = _service.returnJwtTokenResponse(token);

            return tokenHandler;
        }

        public TokenController(IConfiguration configuration, ApiContext context)
        {
            this.configuration = configuration;

            _service = new TokenService();
            _context = context;
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
            
            string tokenHandler = buildTokenResponse(user);

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