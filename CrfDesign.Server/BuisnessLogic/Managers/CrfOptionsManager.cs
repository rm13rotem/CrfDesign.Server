using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Interfaces.Managers;
using BuisnessLogic.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models.Managers
{
    public class CrfOptionsManager : ICrfOptionsManager
    {
        private readonly IInMemoryCrfDataStore _context;

        public CrfOptionsManager(IInMemoryCrfDataStore dataStore)
        {
            _context = dataStore;
        }

        public bool Delete(int id)
        {
            return _context.Delete<CrfOption>(id);
        }

        public CrfOption GetById(int id)
        {
            return _context.CrfOptions
                .FirstOrDefault(m => m.Id == id);
        }

        public List<CrfOption> GetCrfOptionsAsync(CrfOptionFilter crfOptionFilter)
        {
            var result =  _context.CrfOptions;
            if (!string.IsNullOrWhiteSpace(crfOptionFilter.PartialName))
                result = result.Where(x => x.Name.Contains(crfOptionFilter.PartialName)).ToList();
            if (!string.IsNullOrWhiteSpace(crfOptionFilter.PartialCategoryName))
            {
                var categories = _context.CrfOptionCategories.
                    Where(x => x.Name.Contains(crfOptionFilter.PartialCategoryName))
                    .Select(x => x.Id).ToList();
                result = result.Where(x => categories.Contains(x.CrfOptionCategoryId)).ToList();
            }
            if (crfOptionFilter.CategoryId > 0)
                result = result.Where(x => x.CrfOptionCategoryId == crfOptionFilter.CategoryId).ToList();

            return result.OrderBy(x=>x.CrfOptionCategoryId).ToList();
        }

        public async Task<bool> TryInsert(CrfOption entity)
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

        private bool Insert(CrfOption entity)
        {
            entity.Id = _context.CrfOptions.Any() ?
                _context.CrfOptions.Max(x => x.Id) + 1 :
                1;
            return _context.Add(entity);
        }

        public bool TryUndelete(int id)
        {
            return _context.Undelete<CrfOption>(id);
        }

        public bool Duplicate(int? id)
        {
            if (id == null)
                return false;

            try
            {
                var crfOption = _context.CrfOptions.FirstOrDefault(x => x.Id == id);
                if (crfOption == null)
                    return false;

                // var newId = _context.CrfOptions.Max(x => x.Id) + 1;
                var crfOptionNew = new CrfOption()
                {
                    CrfOptionCategoryId = crfOption.CrfOptionCategoryId,
                    Name = crfOption.Name,
                    ModifiedDateTime = DateTime.Now,
                    IsDeleted = false
                };
                return _context.Add(crfOptionNew);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Failed to duplicate id" + id.ToString());
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public bool Update(CrfOption entity)
        {
            return _context.Update(entity);
        }
    }
}
