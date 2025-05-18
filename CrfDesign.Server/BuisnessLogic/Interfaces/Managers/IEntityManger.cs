using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BuisnessLogic.Interfaces.Managers
{
    public interface IEntityManger<C, T>
        where C : DbContext
        where T : IPersistantEntity
    {
        Task<T> GetById(int id);
        Task<bool> Update(T entity);
        Task<bool> TryInsert(T entity);
        Task<bool> Delete(int id);
        Task<bool> TryUndelete(int id);

    }
}
