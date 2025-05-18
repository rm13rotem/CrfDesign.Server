using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
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
            return await _context.CrfOptions
                .Where(x => x.Name.Contains(crfOptionFilter.PartialName))
                .ToListAsync();
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

        public Task<bool> Update(CrfOption entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
