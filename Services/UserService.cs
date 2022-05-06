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

            foreach (var user in users)
                user.Senha = "******";

            return users;
        }

        public async Task<User> returnUserById(int id)
        {
            var user = await repository.GetById(id);

            user.Senha = "******";

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
    }
}
