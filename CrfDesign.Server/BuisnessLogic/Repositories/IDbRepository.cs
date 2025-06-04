using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public interface IDbRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<List<T>> GetAllAsync(bool tracked = true);
        Task<T> GetByIdAsync(int id);
        Task<bool> SaveAsync();
        Task<bool> UpdateAsync(T entity);
    }
}