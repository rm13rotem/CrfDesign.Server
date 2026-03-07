using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.ApiControllerResponses;
using CrfDesign.Server.WebAPI.Models.Filters;
using CrfDesign.Server.WebAPI.Models.Managers;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers.ApiControllers
{
    [Authorize]
    public class CrfPagesApiController : ControllerBase
    {
        private readonly CrfPageManager _manager;
        private readonly ILogger<CrfPagesApiController> _logger;
        private readonly IInMemoryCrfDataStore _dataStore;

        public CrfPagesApiController(UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore,
            IServiceScopeFactory scopeFactory,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CrfPagesApiController> logger)
        {
            _manager = new CrfPageManager(dataStore, userManager, scopeFactory, httpContextAccessor);
            _logger = logger;
            _dataStore = dataStore;
        }

        public async Task<List<CrfPage>> Index(CrfPageFilter filter)
        {
            var pages = await _manager.GetFilteredPagesAsync(filter);

            return pages;
        }

        public List<CrfPageComponent> GoToComponents(int id)
            => _dataStore.CrfPageComponents.Where(x => x.CRFPageId == id).ToList();

        public async Task<CrfPageResponse> Duplicate(int id)
        {
            var result = new CrfPageResponse();
            try
            {
                var page = _manager.GetById(id);
                await _manager.DuplicateAsync(page);
                result.IsSuccess = true;
                result.ErrorMessage = string.Empty;
                var newEntityId = _dataStore.CrfPages.Where(x => x.Name == page.Name &&
                x.Components.Count() == page.Components.Count()).Max(x => x.Id);
                result.EntityId = newEntityId;
                result.CrfPage = _dataStore.CrfPages.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                // log error, optionally show UI error message
                var errorMsg = string.Format("Duplication of page {0} failed", id);
                _logger.LogError(ex, errorMsg);
                result.IsSuccess = false;
                result.ErrorMessage = errorMsg;
            }
            return result;
        }

        public CrfPageResponse Details(int id)
        {
            var page = _manager.GetById(id);
            var result = new CrfPageResponse()
            {
                IsSuccess = (page != null),
                CrfPage = page,
                EntityId = id,
                ErrorMessage = page == null ? "No such Id (CrfPage)" : string.Empty
            };
            return result;
        }

        [HttpPost]
        public async Task<CrfPageResponse> Create(CrfPage page)
        {
            var result = new CrfPageResponse()
            {
                CrfPage = page,
                EntityId = page.Id,
            };

            if (!ModelState.IsValid)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Model is invalid {ModelState.ValidationState.Humanize()}";
                return result;
            }

            result.IsSuccess = await _manager.UpdateAsync(page); // Handles ModifiedDateTime
            if (!result.IsSuccess)
            {
                result.ErrorMessage = page == null ? "No such Id (CrfPage)" : string.Empty;
            }
            else
            {
                result.EntityId = page.Id;
            }
            return result;
        }

        [HttpGet]
        [HttpPost]
        public async Task<CrfPageResponse> Edit(CrfPage page)
        {
            var result = new CrfPageResponse()
            {
                CrfPage = page,
                EntityId = page.Id,
            };
            if (page?.Id == 0 || page?.Id == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Invalid Id to be edited";
                return result;
            }

            if (_manager.IsPageLockedForChanges(page.Id))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Crf Page is locked for change";
                return result;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    result.IsSuccess = await _manager.UpdateAsync(page);
                    if (!result.IsSuccess)
                        result.ErrorMessage = "Crf Page could not update";
                    return result;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "DbUpdate Concurrency Exception in CrfPageController");
                    if (!_manager.Exists(page.Id))
                        
                    result.IsSuccess = false;
                    result.ErrorMessage = ex.Message;
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unknown Exception in CrfPageController");

                    result.IsSuccess = false;
                    result.ErrorMessage = ex.Message;
                    return result;
                }
            }
           result.IsSuccess = false;
            result.ErrorMessage = "Model is invalid";
            return result;
        }

        public async Task<CrfPageResponse> Lock(int id)
        {
            var page = _manager.GetById((int)id);
            var result = new CrfPageResponse()
            {
                CrfPage = page,
                EntityId = id,
            }; 
            result.IsSuccess = await _manager.LockAsync(page);
            return result;
        }

        public async Task<CrfPageResponse> DeleteAsync(int id)
        {
            var page = _manager.GetById(id);
            var result = new CrfPageResponse()
            {
                CrfPage = page,
                EntityId = id,
            };
            if (_manager.IsPageLockedForChanges(id))
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Crf Page {id} is locked";
                return result;
            }
            page.IsDeleted = true;
            result.IsSuccess = await _manager.UpdateAsync(page);
            if (!result.IsSuccess)
            {
                result.ErrorMessage = $"Update of Crf Page Id {id} cannot be updated as delete";
                result.CrfPage.IsDeleted = false;
                return result;
            }
            return result;
        }

        public async Task<CrfPageResponse> Undelete(int id)
        {
            var page = _manager.GetById(id);
            var result = new CrfPageResponse()
            {
                CrfPage = page,
                EntityId = id,
            };
            if (_manager.IsPageLockedForChanges(id))
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Crf Page Id {id} is locked";
                return result;
            }
            page.IsDeleted = false;
            result.IsSuccess = await _manager.UpdateAsync(page);
            if (!result.IsSuccess)
            {
                result.ErrorMessage = $"Update of Crf Page Id {id} cannot be updated as not delete";
                result.CrfPage.IsDeleted = true;
                return result;
            }
            return result;
        }

        [Authorize(Roles = "Admin")]
        public bool ReloadCache()
        {
            _manager.DataStore_Refresh();
            return true;
        }

    }
}
