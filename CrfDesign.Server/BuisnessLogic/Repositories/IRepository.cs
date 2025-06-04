using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T?>> GetAllAsync(bool tracked = true);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
    }
}
