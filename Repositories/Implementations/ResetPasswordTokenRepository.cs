using CrudUsuarios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUsuarios.Repositories.Implementations
{
    public class ResetPasswordTokenRepository : BaseRepository<ResetPasswordToken>, IResetPasswordTokenRepository
    {
        public ResetPasswordTokenRepository(ApiContext context) : base(context) { }

        public async Task<ResetPasswordToken> GetTokenByTokenString(string token)
        {
            return await context.ResetPasswordToken
                .Where(resetToken => resetToken.Token == token)
                .FirstOrDefaultAsync();
        }
    }
}
