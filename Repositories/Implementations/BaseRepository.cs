using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudUsuarios.Models;

namespace CrudUsuarios.Repositories
{
    public class BaseRepository<Type> : IBaseRepository<Type> where Type : class
    {
        protected ApiContext context = null;
        public BaseRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task Delete(int Id)
        {
            Type entity = await this.GetById(Id);
            
            context
                .Set<Type>()
                .Remove(entity);
            
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Type>> GetAll()
        {
            return await context.Set<Type>().AsNoTracking().ToListAsync();
        }

        public async Task<Type> GetById(int Id)
        {
            return await context.Set<Type>().FindAsync(Id);
        }

        public async Task Insert(Type obj)
        {
            await context.Set<Type>().AddAsync(obj);

            await context.SaveChangesAsync();
        }

        public async Task Update(Type obj)
        {
            context.Set<Type>().Update(obj);
            
            await context.SaveChangesAsync();
        }
    }
}
