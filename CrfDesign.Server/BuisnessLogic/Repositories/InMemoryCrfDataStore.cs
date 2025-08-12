using BuisnessLogic.DataContext;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public class InMemoryCrfDataStore : IInMemoryCrfDataStore
    {
        // In-memory collections
        public List<CrfPage> CrfPages { get; private set; }
        public List<CrfPageComponent> CrfPageComponents { get; private set; }
        public List<CrfOption> CrfOptions { get; private set; }
        public List<CrfOptionCategory> CrfOptionCategories { get; private set; }
        public List<QuestionType> QuestionTypes { get; private set; }

        private readonly object _lock = new();
        private CrfDesignContext _context;

        public InMemoryCrfDataStore(IServiceScopeFactory scopeFactory)
        {
            Refresh(scopeFactory);
        }

        public void Refresh(IServiceScopeFactory scopeFactory)
        {
            // Create a temporary scope to get the scoped DbContext
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CrfDesignContext>();
            
            lock (_lock)
            {
                _context = context;
                LoadAllData();
            }
        }

        public void LoadAllData()
        {
            CrfPages = _context.CrfPages.ToList();
            CrfPageComponents = _context.CrfPageComponents.ToList();
            CrfOptions = _context.CrfOptions.ToList();
            CrfOptionCategories = _context.CrfOptionCategories.ToList();
            QuestionTypes = _context.QuestionTypes.ToList();
        }

        // ADD
        public bool Add<T>(T entity) where T : class, IPersistantEntity
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();

                // Add to memory
                GetList<T>().Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // UPDATE
        public bool Update<T>(T entity) where T : class
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
                return true; // Entity already referenced in memory list
            }
            catch
            {
                return false;
            }
        }

        // DELETE
        public bool Delete<T>(int id) where T : class, IPersistantEntity
        {
            var entity = GetList<T>().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                return Update(entity);
            }
            return false;
        }

        // UNDELETE
        public bool Undelete<T>(int id) where T : class, IPersistantEntity
        {
            var entity = GetList<T>().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                Update(entity);
            }
            return false;
        }

        // Internal helper to map type to the right list
        public List<T> GetList<T>() where T : class, IPersistantEntity
        {
            if (typeof(T) == typeof(CrfPage)) return CrfPages as List<T>;
            if (typeof(T) == typeof(CrfPageComponent)) return CrfPageComponents as List<T>;
            if (typeof(T) == typeof(CrfOption)) return CrfOptions as List<T>;
            if (typeof(T) == typeof(CrfOptionCategory)) return CrfOptionCategories as List<T>;
            if (typeof(T) == typeof(QuestionType)) return QuestionTypes as List<T>;

            throw new InvalidOperationException($"Unknown type {typeof(T).Name}");
        }
    }
}
