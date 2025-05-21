using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models.Managers
{
    public class CrfOptionsManager : ICrfOptionsManager, IEntityManger<CrfDesignContext, CrfOption>
    {
        private readonly CrfDesignContext _context;

        public CrfOptionsManager(CrfDesignContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var existingEntity = await GetById(id);
            if (existingEntity == null)
                return false;
            else existingEntity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CrfOption> GetById(int id)
        {
            return await _context.CrfOptions
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<CrfOption>> GetCrfOptionsAsync(CrfOptionFilter crfOptionFilter)
        {
            var result = await _context.CrfOptions.ToListAsync();
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

            return result;
        }

        public async Task<bool> TryInsert(CrfOption entity)
        {
            try
            {
                return await Insert(entity);
            }
            catch (System.Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Task.Delay(i * 5000).Wait();
                        return await Insert(entity);
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            return false;
        }

        private async Task<bool> Insert(CrfOption entity)
        {
            entity.Id = _context.CrfOptions.Any() ?
                _context.CrfOptions.Max(x => x.Id) + 1 :
                1;
            _context.CrfOptions.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryUndelete(int id)
        {
            var existingEntity = await GetById(id);
            if (existingEntity == null)
                return false;
            else existingEntity.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DuplicateAsync(int? id)
        {
            if (id == null)
                return false;

            try
            {
                var crfOption = await _context.CrfOptions.FindAsync(id);
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
                _context.Add(crfOptionNew);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Failed to duplicate id" + id.ToString());
                Console.WriteLine(e.Message);
            }
            return true;
        }

        public async Task<bool> Update(CrfOption entity)
        {
            var existingEntity = await GetById(entity.Id);
            if (existingEntity == null)
                return false;

            _context.CrfOptions.Attach(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
