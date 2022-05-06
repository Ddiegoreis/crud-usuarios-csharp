using CrudUsuarios.Models;
using CrudUsuarios.Repositories;
using CrudUsuarios.Utils;
using System;
using System.Threading.Tasks;

namespace CrudUsuarios.Services
{
    public class PasswordChangeService
    {
        private readonly IUserRepository repository;
        public PasswordChangeService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task updateUser(string senhaNova, User user)
        {
            user.Senha = EncryptPassword.execute(senhaNova);

            await repository.UpdateUserPassword(user);
        }

        public async Task<string> returnResetToken(User user)
        {
            string token = TokenUtil.Token;

            ResetPasswordToken reset = new ResetPasswordToken()
            {
                Cadastro = DateTime.Now,
                Token = EncryptPassword.execute(token),
                Validade = TokenUtil.validadeToken
            };

            user.ResetPasswordToken.Add(reset);

            await repository.Update(user);

            return token;
        }
    }
}
