using CrudUsuarios.Models;
using CrudUsuarios.Repositories;
using CrudUsuarios.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CrudUsuarios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository repository;

        public ChangePasswordController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UserResetPassword user)
        {
            User userBanco = await repository.GetById(user.Id);

            if ((user == null) || (user.Id == 0) || (userBanco == null))
            {
                return BadRequest("Usuário inválido");
            }

            try
            {
                ResetPasswordToken token = repository.GetLastToken(userBanco);

                if (!(TokenUtil.IsValid(token)) || (token.Token != EncryptPassword.execute(user.Token)))
                {
                    return BadRequest("Token expirado ou inválido");
                }
                else if (userBanco.Senha != EncryptPassword.execute(user.Senha))
                {
                    return BadRequest("Senha original inválida");
                }
                else if (user.SenhaNova.Trim() == "")
                {
                    return BadRequest("Senha inválida");
                }

                userBanco.Senha = EncryptPassword.execute(user.SenhaNova);

                await repository.UpdateUserPassword(userBanco);

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Senha alterada com sucesso");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            string token = TokenUtil.Token;

            var user = await repository.GetById(id);

            if (user == null)
            {
                return NotFound("Usuario não encontrado pelo id informado");
            }

            ResetPasswordToken reset = new ResetPasswordToken()
            {
                Cadastro = DateTime.Now,
                Token = EncryptPassword.execute(token),
                Validade = TokenUtil.validadeToken
            };

            user.ResetPasswordToken.Add(reset);

            await repository.Update(user);

            return Ok($"Seu token para reset de senha é {token}");
        }
    }
}