using System.Threading.Tasks;
using CrudUsuarios.Models;

namespace CrudUsuarios.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task UpdateUserName(User user);
        public Task UpdateUserPassword(User user);
        public void LoadPendencias(User user);
        public ResetPasswordToken GetLastToken(User user);
    }
}
