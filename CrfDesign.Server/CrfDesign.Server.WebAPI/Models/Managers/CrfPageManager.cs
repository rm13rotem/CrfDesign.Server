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
using BuisnessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrfDesign.Server.WebAPI.Models.Managers
{
    public class CrfPageManager
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly UserManager<Investigator> _userManager;
        private readonly IServiceScopeFactory _scopeFactory;
        public CrfPageManager(IInMemoryCrfDataStore dataStore, UserManager<Investigator> userManager,
            IServiceScopeFactory scopeFactory)
        {
            _context = dataStore    ;
            _userManager = userManager;
            _scopeFactory = scopeFactory;
        }

        public async Task<List<CrfPage>> GetFilteredPagesAsync(CrfPageFilter filter)
        {
            var pages = _context.CrfPages;

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

        public CrfPage GetById(int id)
        {
            return _context.CrfPages.FirstOrDefault(x => x.Id == id);
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
                }
            }
        }

        public bool Update(CrfPage page)
        {
            page.ModifiedDateTime = DateTime.UtcNow;
            return _context.Update(page);
        }

        public bool Exists(int id)
        {
            return _context.CrfPages.Any(e => e.Id == id);
        }

        public void StoreRefresh()
        {
            _context.Refresh(_scopeFactory);
        }
    }

}
