using CrudUsuarios.Models;
using CrudUsuarios.Repositories;
using CrudUsuarios.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudUsuarios.Services
{
    public class UserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<User>> returnAllUsers()
        {
            var users = await repository.GetAll();

            if (users == null)
                return null;

            return users;
        }

        public async Task<User> returnUserById(int id)
        {
            var user = await repository.GetById(id);

            if (user == null)
                return null;

            return user;
        }

        public async Task createUser(User user)
        {
            user.Senha = EncryptPassword.execute(user.Senha);

            await repository.Insert(user);
        }

        public async Task updateUser(User user)
        {
            await repository.UpdateUserName(user);
        }

        public async Task deleteUser(int id)
        {


            await repository.Delete(id);
        }

        public ResetPasswordToken returnLastToken(User user)
        {
            var lastToken = repository.GetLastToken(user);

            return lastToken;
        }
    }
}
