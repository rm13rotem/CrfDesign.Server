using BuisnessLogic.DataContext;
using BuisnessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public class DbRepository<T> 
        where T : class, IPersistantEntity
    {
        internal IInMemoryCrfDataStore _context;
        internal List<T> _dbSet;

        public DbRepository(IInMemoryCrfDataStore dataStore)
        {
            _context = dataStore;
            _dbSet = _context.GetList<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync(bool tracked = true)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            return await _context.AddAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _context.DeleteAsync<T>(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _context.UpdateAsync<T>(entity);
        }

    }
}
