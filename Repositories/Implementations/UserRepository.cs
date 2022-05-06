using System.Linq;
using System.Threading.Tasks;
using CrudUsuarios.Models;

namespace CrudUsuarios.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApiContext context) : base(context) { }

        public async Task UpdateUserName(User user)
        {
            context.Entry(user)
                .Property(user => user.Nome)
                .IsModified = true;
            
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserPassword(User user)
        {
            context.Entry(user)
                .Property(user => user.Senha)
                .IsModified = true;
            
            await context.SaveChangesAsync();
        }

        public void LoadPendencias(User user)
        {
            context.Entry(user)
                .Collection(user => user.ResetPasswordToken)
                .Load();
        }

        public ResetPasswordToken GetLastToken(User user)
        {
            LoadPendencias(user);
            
            return user.ResetPasswordToken
                .OrderByDescending(resetPassword => resetPassword.Cadastro)
                .FirstOrDefault();
        }
    }
}
