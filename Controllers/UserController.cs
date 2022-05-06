using CrudUsuarios.Models;
using CrudUsuarios.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudUsuarios.Utils;
using CrudUsuarios.Services;

namespace CrudUsuarios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService service;
        private readonly IUserRepository repository;
        
        public UserController(IUserRepository repository)
        {
            this.repository = repository;

            service = new UserService(repository);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await service.returnAllUsers();

            if (users == null)
            {
                return BadRequest();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await service.returnUserById(id);
            
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Usuário inválido");
            }

            if (user.Id != 0)
            {
                return BadRequest("Id não deve ser definido para o insert/create.");
            }

            await service.createUser(user);

            return CreatedAtAction(nameof(GetUser), new { Id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(User user)
        {
            if ((user == null) || (user.Id == 0))
            {
                return NotFound("Usuário informado não encontrado");
            }

            try
            {
                await this.service.updateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Usuario atualizado");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            User user = await this.service.returnUserById(id);

            if (user == null)
            {
                return NotFound("Usuário informado não encontrado");
            }

            await service.deleteUser(id);

            return Ok(user);
        }
    }
}
