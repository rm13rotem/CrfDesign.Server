using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
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
        private readonly IInMemoryCrfDataStore _context;
        private readonly UserManager<Investigator> _userManager;

        public CrfOptionCategoryManager(IInMemoryCrfDataStore dataStore, UserManager<Investigator> userManager)
        {
            _context = dataStore;
            _userManager = userManager;
        }

        public async Task<List<CrfOptionCategory>> GetAllAsync(CrfOptionCategoriesFilter filter)
        {
            try
            {
                filter.TotalLines = _context.CrfOptionCategories.Count();

                var categories = _context.CrfOptionCategories
                    .OrderBy(x => x.Id)
                    .Skip(filter.Page - 1)
                    .Take(filter.NLines)
                    .ToList();

                if (!categories.Any())
                    categories = _context.CrfOptionCategories;

                foreach (var item in categories)
                {
                    if (!string.IsNullOrEmpty(item.LastUpdatorUserId))
                    {
                        var user = await _userManager.FindByIdAsync(item.LastUpdatorUserId);
                        item.LastUpdatorUserId = user != null ? $"{user.FirstName} {user.LastName}" : "Unknown";
                    }
                }

                return categories;
            }
            catch
            {
                return null;
            }
        }

        public CrfOptionCategory GetDetails(int? id)
        {
            if (id == null) return null;

            return _context.CrfOptionCategories.FirstOrDefault(m => m.Id == id);
        }

        public async Task<bool> CreateAsync(CrfOptionCategory model)
        {
            try
            {
                return await _context.AddAsync(model);
            }
            catch { return false; }
        }

        public async Task<CrfOptionCategory> GetById(int? id)
        {
            if (id == null) return null;
            return _context.CrfOptionCategories.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> EditAsync(CrfOptionCategory model)
        {
            try
            {
                return await _context.UpdateAsync(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CrfOptionCategories.Any(e => e.Id == model.Id))
                    return false;

                throw;
            }
        }

        public CrfOptionCategory GetDelete(int? id)
        {
            if (id == null) return null;
            return _context.CrfOptionCategories.FirstOrDefault(m => m.Id == id);
        }

        public void DeleteConfirmed(int id)
        {
            _context.DeleteAsync<CrfOptionCategory>(id);
        }

        public void UnDelete(int id)
        {
            _context.UndeleteAsync<CrfOptionCategory>(id);
        }
    }

}
