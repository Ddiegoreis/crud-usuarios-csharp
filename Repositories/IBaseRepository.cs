using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudUsuarios.Repositories
{
    public interface IBaseRepository<Type> where Type : class
    {
        Task<IEnumerable<Type>> GetAll();
        Task<Type> GetById(int Id);
        Task Insert(Type obj);
        Task Update(Type obj);
        Task Delete(int Id);
    }
}
