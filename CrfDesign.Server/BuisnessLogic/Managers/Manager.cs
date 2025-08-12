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
            return _context.Delete<T>(id);
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
                return Insert(entity);
            }
            catch (System.Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Task.Delay(i * 5000).Wait();
                        return Insert(entity);
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            return false;
        }

        private bool Insert(T entity)
        {
            return _context.Add(entity);
        }

        public bool TryUndelete(int id)
        {
            return _context.Undelete<T>(id);
        }

        public bool Update(T entity)
        {
            return _context.Update(entity);
        }

   
        public virtual T Duplicate(T entity)
        {
            entity.Id = 0;
            _context.Add(entity);
            return entity; // new entity
        }
    }
}
