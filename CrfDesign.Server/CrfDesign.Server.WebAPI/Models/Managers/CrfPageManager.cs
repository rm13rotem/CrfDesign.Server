using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using CrfDesign.Server.WebAPI.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BuisnessLogic.Models.Managers;

namespace CrfDesign.Server.WebAPI.Models.Managers
{
    public class CrfPageManager
    {
        private readonly CrfDesignContext _context;
        private readonly UserManager<Investigator> _userManager;

        public CrfPageManager(CrfDesignContext context, UserManager<Investigator> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<CrfPage>> GetFilteredPagesAsync(CrfPageFilter filter)
        {
            var pages = await _context.CrfPages.ToListAsync();

            if (!string.IsNullOrEmpty(filter.PartialName))
                pages = pages.Where(x => x.Name.Contains(filter.PartialName)).ToList();

            foreach (var page in pages)
            {
                if (!string.IsNullOrEmpty(page.LastUpdatorUserId))
                {
                    var user = await _userManager.FindByIdAsync(page.LastUpdatorUserId);
                    page.LastUpdatorUserId = user != null ? $"{user.FirstName} {user.LastName}" : "Unknown";
                }
            }

            return pages;
        }

        public async Task<CrfPage> GetByIdAsync(int id)
        {
            return await _context.CrfPages.FindAsync(id);
        }

        public async Task DuplicateAsync(CrfPage page)
        {
            var duplicate = new CrfPage
            {
                Name = page.Name + "_Copy",
                Description = page.Description,
                StudyId = page.StudyId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                IsLockedForChanges = false,
                ModifiedDateTime = page.ModifiedDateTime
            };

            _context.CrfPages.Add(duplicate);
            await _context.SaveChangesAsync();

            var page_Components = _context.CrfPageComponents.
                Where(x => x.CRFPageId == page.Id).ToList();
            var componentManager = new Manager<CrfPageComponent>(_context);

            foreach (var component in page_Components)
            {
                var newComponent = componentManager.Duplicate(component);
                newComponent.CRFPageId = duplicate.Id;// belongs to new CRFPage
                //newComponent.CrfPage = duplicate;
                var newDbEntity = newComponent.ToNewEntity() as CrfPageComponent;
                if (newDbEntity != null)
                {
                    _context.CrfPageComponents.Add(newDbEntity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(CrfPage page)
        {
            page.ModifiedDateTime = DateTime.UtcNow;
            _context.Update(page);
        }

        public bool Exists(int id)
        {
            return _context.CrfPages.Any(e => e.Id == id);
        }
    }

}
