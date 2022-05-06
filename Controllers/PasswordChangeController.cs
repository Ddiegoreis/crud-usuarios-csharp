using CrudUsuarios.Models;
using CrudUsuarios.Repositories;
using CrudUsuarios.Services;
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
        private readonly PasswordChangeService passwordService;
        private readonly UserService userService;

        public ChangePasswordController(IUserRepository repository)
        {
            this.repository = repository;

            passwordService = new PasswordChangeService(repository);
            userService = new UserService(repository);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UserResetPassword user)
        {
            User currentUser = await userService.returnUserById(user.Id);

            if ((user == null) || (user.Id == 0) || (currentUser == null))
            {
                return BadRequest("Usuário inválido");
            }

            try
            {
                ResetPasswordToken token = userService.returnLastToken(currentUser);

                if (!(TokenUtil.IsValid(token)) || (token.Token != EncryptPassword.execute(user.Token)))
                {
                    return BadRequest("Token expirado ou inválido");
                }
                else if (user.SenhaNova.Trim() == "")
                {
                    return BadRequest("Senha inválida");
                }

                await passwordService.updateUser(user.SenhaNova, currentUser);

                await repository.UpdateUserPassword(currentUser);
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
            var user = await userService.returnUserById(id);

            if (user == null)
            {
                return NotFound("Usuario não encontrado pelo id informado");
            }

            string resetToken = await passwordService.returnResetToken(user);

            return Ok($"Sua chave para alteração é {resetToken}");
        }
    }
}