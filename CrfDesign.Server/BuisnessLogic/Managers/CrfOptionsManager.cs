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
using Microsoft.Extensions.Logging;

namespace BuisnessLogic.Models.Managers
{
    public class CrfOptionsManager : ICrfOptionsManager
    {
        private readonly IInMemoryCrfDataStore _context;

        public CrfOptionsManager(IInMemoryCrfDataStore dataStore, ILogger<CrfOptionsManager> _logger)
        {
            _context = dataStore;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _context.DeleteAsync<CrfOption>(id);
        }

        public CrfOption GetById(int id)
        {
            return _context.CrfOptions
                .FirstOrDefault(m => m.Id == id);
        }

        public List<CrfOption> GetCrfOptionsAsync(CrfOptionFilter crfOptionFilter)
        {
            var result = _context.CrfOptions;
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

            return result.OrderBy(x => x.CrfOptionCategoryId).ToList();
        }

        public async Task<bool> TryInsert(CrfOption entity)
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
                        await Task.Delay(i * 5000);
                        return await InsertAsync(entity);
                    }
                    catch (System.Exception)
                    {
                        // todo : add _logger.LogException(...)
                    }
                }
            }
            return false;
        }

        private async Task<bool> InsertAsync(CrfOption entity)
        {
            entity.Id = _context.CrfOptions.Any() ?
                _context.CrfOptions.Max(x => x.Id) + 1 :
                1;
            return await _context.AddAsync(entity);
        }

        public async Task<bool> TryUndeleteAsync(int id)
        {
            return await _context.UndeleteAsync<CrfOption>(id);
        }

        public async Task<bool> DuplicateAsync(int id)
        {
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
                    IsDeleted = false,
                    IsLockedForChanges = false
                };
                return await _context.AddAsync(crfOptionNew);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Failed to duplicate id" + id.ToString());
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<bool> UpdateAsync(CrfOption entity)
        {
            try
            {
                return await _context.UpdateAsync(entity);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Failed to Update CrfOption id=" + entity?.Id.ToString());
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
