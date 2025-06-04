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
    public class DbRepository<T> : IRepository<T>, IDbRepository<T> where T : class
    {
        internal CrfDesignContext context;
        internal DbSet<T> _dbSet;

        public DbRepository(CrfDesignContext context)
        {
            this.context = context;
            this._dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
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
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete != null)
            {
                if (entityToDelete is IPersistantEntity dbEntity)
                {
                    dbEntity.IsDeleted = true;
                    return await SaveAsync();
                }
            }
            return false;
        }



        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
