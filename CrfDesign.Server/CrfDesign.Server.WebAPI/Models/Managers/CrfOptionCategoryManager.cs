using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.Managers
{
    public class CrfOptionCategoryManager
    {
        private readonly CrfDesignContext _context;
        private readonly UserManager<Investigator> _userManager;

        public CrfOptionCategoryManager(CrfDesignContext context, UserManager<Investigator> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<CrfOptionCategory>> GetAllAsync(CrfOptionCategoriesFilter filter)
        {
            try
            {
                filter.TotalLines = _context.CrfOptionCategories.Count();

                var result = await _context.CrfOptionCategories
                    .OrderBy(x => x.Id)
                    .Skip(filter.Page - 1)
                    .Take(filter.NLines)
                    .ToListAsync();

                if (!result.Any())
                    result = await _context.CrfOptionCategories.ToListAsync();

                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.LastUpdatorUserId))
                    {
                        var user = await _userManager.FindByIdAsync(item.LastUpdatorUserId);
                        item.LastUpdatorUserId = user != null ? $"{user.FirstName} {user.LastName}" : "Unknown";
                    }
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<CrfOptionCategory> GetDetailsAsync(int? id)
        {
            if (id == null) return null;

            return await _context.CrfOptionCategories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> CreateAsync(CrfOptionCategory model)
        {
            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<CrfOptionCategory> GetEditAsync(int? id)
        {
            if (id == null) return null;
            return await _context.CrfOptionCategories.FindAsync(id);
        }

        public async Task<bool> EditAsync(CrfOptionCategory model)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CrfOptionCategories.Any(e => e.Id == model.Id))
                    return false;

                throw;
            }
        }

        public async Task<CrfOptionCategory> GetDeleteAsync(int? id)
        {
            if (id == null) return null;
            return await _context.CrfOptionCategories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteConfirmedAsync(int id)
        {
            var entity = await _context.CrfOptionCategories.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }

}
