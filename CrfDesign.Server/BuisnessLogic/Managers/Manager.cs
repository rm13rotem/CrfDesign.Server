using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Interfaces.Managers;
using BuisnessLogic.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models.Managers
{
    public class Manager<T>
        where T : class, IPersistantEntity, new()
    {
        protected IInMemoryCrfDataStore _context { get; set; }

        public Manager(IInMemoryCrfDataStore dataStore)
        {
            _context = dataStore;
        }

        public async Task<bool> Delete(int id)
        {
            return await _context.DeleteAsync<T>(id);
        }

        public T GetById(int id)
        {
            return _context.GetList<T>()
                .FirstOrDefault(m => m.Id == id);
        }

        public List<CrfOption> GetCrfOptions(CrfOptionFilter crfOptionFilter)
        {
            return _context.CrfOptions
                .Where(x => x.Name.Contains(crfOptionFilter.PartialName))
                .ToList();
        }

        public async Task<bool> TryInsert(T entity)
        {
            try
            {
                return await InsertAsync(entity);
            }
            catch (System.Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Task.Delay(i * 5000).Wait();
                        return await InsertAsync(entity);
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            return false;
        }

        private async Task<bool> InsertAsync(T entity)
        {
            return await _context.AddAsync(entity);
        }

        public async Task<bool> TryUndeleteAsync(int id)
        {
            return await _context.UndeleteAsync<T>(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _context.UpdateAsync(entity);
        }

   
        public virtual async Task<T> DuplicateAsync(T entity)
        {
            entity.Id = 0;
            await _context.AddAsync(entity);
            return entity; // new entity
        }

        
    }
}
