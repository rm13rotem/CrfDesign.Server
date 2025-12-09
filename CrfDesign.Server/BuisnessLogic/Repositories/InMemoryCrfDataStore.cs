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
        private readonly IServiceScopeFactory _scopeFactory;

        public InMemoryCrfDataStore(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            Refresh();
        }

        public void Refresh()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CrfDesignContext>();

            lock (_lock)
            {
                LoadAllData(context);
            }
        }

        private void LoadAllData(CrfDesignContext context)
        {
            CrfPages = context.CrfPages.ToList();
            CrfPageComponents = context.CrfPageComponents.ToList();
            CrfOptions = context.CrfOptions.ToList();
            CrfOptionCategories = context.CrfOptionCategories.ToList();
            QuestionTypes = context.QuestionTypes.ToList();
        }

        // ADD
        public bool Add<T>(T entity) where T : class, IPersistantEntity
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<CrfDesignContext>();

                context.Set<T>().Add(entity);
                context.SaveChanges();

                // Add to memory
                lock (_lock)
                {
                    GetList<T>().Add(entity);
                }
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
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<CrfDesignContext>();

                context.Set<T>().Update(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                return Update(entity);
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