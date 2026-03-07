using BuisnessLogic.Filters;
using BuisnessLogic.Interfaces.Managers;
using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.ApiControllerResponses;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers.ApiControllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CrfOptionsApiController : ControllerBase
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly ICrfOptionsManager _manager;
        private readonly CrfPageManager _pageManager;
        private readonly UserManager<Investigator> _userManager;
        private readonly ILogger<CrfOptionsController> _logger;

        public CrfOptionsApiController(
            UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore,
            IServiceScopeFactory scopeFactory,
            IHttpContextAccessor httpContextAccessor,
            ICrfOptionsManager manager,
            ILogger<CrfOptionsController> logger)
        {
            _context = dataStore;
            _logger = logger;
            _manager = manager;
            _userManager = userManager;
            _pageManager = new CrfPageManager(dataStore, userManager, scopeFactory, httpContextAccessor);

        }

        // GET: CrfOptions
        public async Task<List<CrfOption>> Index(CrfOptionFilter filter)
        {
            if (filter == null) filter = new CrfOptionFilter();
            var dbresults = _manager.GetCrfOptionsAsync(filter);
            return dbresults;
        }

        // GET: CrfOptions/Details/5
        public CrfOptionResponse Details(int id)
        {
            var crfOption = _manager.GetById(id);
            var crfOptionResponse = new CrfOptionResponse() { OptionEntity = crfOption };
            crfOptionResponse.EntityId = id;
            if (crfOption == null)
            {
                crfOptionResponse.IsSuccess = false;
                crfOptionResponse.ErrorMessage = $"No option found with id {id}";
                return crfOptionResponse;
            }

            crfOptionResponse.IsSuccess = true;
            crfOptionResponse.ErrorMessage = string.Empty;
            return crfOptionResponse;
        }

        // GET: CrfOptions/Create
        [HttpPost]
        public async Task<CrfOptionResponse> Create(CrfOption crfOption)
        {
            CrfPage crfPage = GetLockedCrfByOptionId(crfOption.Id);
            var crfOptionResponse = new CrfOptionResponse() { OptionEntity = crfOption };
            crfOptionResponse.EntityId = crfOption?.Id ?? 0;
            if (crfPage != null)
            {
                crfOptionResponse.IsSuccess = false;
                crfOptionResponse.ErrorMessage = $"Option cannot be created because it is being used in CRF page {crfPage.Name} which is locked for changes.";
                return crfOptionResponse;
            }

            if (ModelState.IsValid)
            {
                crfOptionResponse.IsSuccess = await _context.AddAsync(crfOption);
                if (!crfOptionResponse.IsSuccess)
                {
                    crfOptionResponse.ErrorMessage = $"Failed to create option with name {crfOption.Name}";
                }
                return crfOptionResponse;
            }

            crfOptionResponse.IsSuccess = false;
            crfOptionResponse.ErrorMessage = $"model of {nameof(CrfOptionResponse)} is not valid";
            return crfOptionResponse;
        }



        // GET: CrfOptions/Duplicate/5
        [HttpGet]
        public async Task<CrfOptionResponse> Duplicate(int id)
        {
            CrfPage crfPage = GetLockedCrfByOptionId(id);
            var crfOptionResponse = new CrfOptionResponse();
            crfOptionResponse.EntityId = id;
            if (crfPage != null)
            {
                crfOptionResponse.IsSuccess = false;
                crfOptionResponse.ErrorMessage = $"Option cannot be created because it is being used in CRF page {crfPage.Name} which is locked for changes.";
                return crfOptionResponse;
            }

            bool isSuccess = await _manager.DuplicateAsync(id);
            crfOptionResponse.IsSuccess = isSuccess;
            if (!isSuccess)
            {
                crfOptionResponse.ErrorMessage = $"Failed to duplicate option with id {id}";
                return crfOptionResponse;
            }

            return crfOptionResponse;
        }

        private CrfPage GetLockedCrfByOptionId(int? id)
        {

            CrfOption crfOption = _context.CrfOptions.Find(x => x.Id == id);
            if (crfOption == null) return null;

            var category = _context.CrfOptionCategories.Find(x => x.Id == crfOption.CrfOptionCategoryId);
            if (category == null) return null;

            return GetLockedCrfByCategory(category.Id);
        }

        private CrfPage GetLockedCrfByCategory(int crfOptionCategoryId)
        {

            var components = _context.CrfPageComponents
                .Where(x => x.CategoryId == crfOptionCategoryId).ToList();
            //maybe many components
            if (components.Count == 0)
                return null;

            var componentCrfPageIds = components.Select(x => x.CRFPageId).Distinct().ToList();
            var crfPages = _context.CrfPages
                .Where(x => x.IsLockedForChanges)
                .ToList().Where(x => componentCrfPageIds.Any(y => y == x.Id)).ToList();

            // may be many CRF pages with this specific option drop down in them
            CrfPage lockedCRF = crfPages.FirstOrDefault(x => x.IsLockedForChanges == true);
            return lockedCRF;
        }




        // POST: CrfOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [HttpPut]
        public async Task<CrfOptionResponse> Edit(CrfOption crfOption)
        {
            var response = new CrfOptionResponse() { OptionEntity = crfOption, EntityId = crfOption.Id };
            CrfPage crfPage = GetLockedCrfByOptionId(crfOption.Id);
            if (_pageManager.IsPageLockedForChanges(crfPage.Id))
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Option cannot be edited because it is being used in CRF page {crfPage.Name} which is locked for changes.";
                return response;
            }

            bool isSuccess;
            if (ModelState.IsValid)
            {
                try
                {
                    crfOption.ModifiedDateTime = DateTime.UtcNow;
                    response.IsSuccess = await _context.UpdateAsync(crfOption);
                    return response;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrfOptionExists(crfOption.Id))
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = $"No option found with id {crfOption.Id}";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = $"Failed to update option with id {crfOption.Id} due to concurrency issue. Please try again.";
                    }
                }
            }

            return response;
        }

        // GET: CrfOptions/Delete/5
        [HttpPost]
        [HttpDelete]
        [HttpGet]
        public async Task<CrfOptionResponse> Delete(int id)
        {
            var response = new CrfOptionResponse() { EntityId = id };

            CrfPage crfPage = GetLockedCrfByOptionId(id);
            if (_pageManager.IsPageLockedForChanges(crfPage.Id))
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Option cannot be edited because it is being used in CRF page {crfPage.Name} which is locked for changes.";
                return response;
            }

            var crfOption = _context.CrfOptions.FirstOrDefault(m => m.Id == id);
            response.OptionEntity = crfOption;
            if (crfOption == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"No option found with id {id}";
                return response;
            }

            response.IsSuccess = await _context.DeleteAsync<CrfOption>(id);
            if (!response.IsSuccess)
            {
                response.ErrorMessage = $"Failed to delete option with id {id}";
                return response;
            }
            return response;
        }



        public async Task<CrfOptionResponse> UndeleteAsync(int id)
        {

            var response = new CrfOptionResponse() { EntityId = id };
            response.IsSuccess = await _context.UndeleteAsync<CrfOption>(id);
            if (!response.IsSuccess)
            {
                response.ErrorMessage = $"Failed to undelete option with id {id}";
                return response;
            }
            return response;
        }

        private bool CrfOptionExists(int id)
        {
            return _context.CrfOptions.Any(e => e.Id == id);
        }
    }
}
