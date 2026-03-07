using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.ApiControllerResponses;
using CrfDesign.Server.WebAPI.Models.Filters;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers.ApiControllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CrfPageComponentsApiController : ControllerBase
    {
        private readonly IInMemoryCrfDataStore _dataStore;
        private readonly CrfPageComponentManager _manager;
        private readonly UserManager<Investigator> _userManager;

        public CrfPageComponentsApiController(UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore, IServiceScopeFactory scopedFactory)
        {
            _dataStore = dataStore;
            _manager = new CrfPageComponentManager(_dataStore, userManager, scopedFactory);
            _userManager = userManager;
        }

        // GET: CrfPageComponents
        public async Task<List<CrfPageComponent>> Index(CrfPageComponentFilter filter)
        {
            if (filter == null)
            {
                filter = _manager.GetDefaultFilter(filter);
            }
            return _manager.GetItems(filter);
        }

        // GET: CrfPageComponents/Details/5
        public async Task<CrfPageComponentResponse> Details(int id)
        {
            var response = new CrfPageComponentResponse()
            {
                EntityId = id
            };

            response.Entity = _dataStore.CrfPageComponents
                .FirstOrDefault(m => m.Id == id);

            if (response.Entity == null)
            {

                _dataStore.Refresh();
                response.Entity = _dataStore.CrfPageComponents
                   .FirstOrDefault(m => m.Id == id);

                if (response.Entity == null)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = $"CrfPageComponent with id {id} not found.";
                    return response;
                }
            }
            // else
            response.IsSuccess = true;
            if (response.Entity.ModifiedDateTime < new DateTime(1900, 1, 1))
            {
                response.Entity.ModifiedDateTime = DateTime.UtcNow;
                await _dataStore.UpdateAsync(response.Entity);
            }
            return response;
        }

        // GET: CrfPageComponents/Create


        // POST: CrfPageComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<CrfPageComponentResponse> Create(CrfPageComponent crfPageComponent)
        {
            var response = new CrfPageComponentResponse()
            {
                EntityId = crfPageComponent?.Id ?? 0,
                IsSuccess = false,
                Entity = crfPageComponent
            };

            if (ModelState.IsValid)
            {
                crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
                response.IsSuccess = await _dataStore.AddAsync(crfPageComponent);
                if (!response.IsSuccess)
                    response.ErrorMessage = "Could not add crf component to database";
                return response;
            }
            else
            {
                response.ErrorMessage = "Model state is invalid.";
                return response;
            }
        }

        // GET: CrfPageComponents/Duplicate/5
        public async Task<CrfPageComponentResponse> DuplicateAsync(int id)
        {
            var response = new CrfPageComponentResponse()
            {
                EntityId = id,
                IsSuccess = false
            };
            var crfPageComponent = _dataStore.CrfPageComponents.FirstOrDefault(x => x.Id == id);
            if (crfPageComponent == null)
            {
                response.ErrorMessage = $"CrfPageComponent with id {id} not found.";
                return response;
            }
            // else
            response.Entity = await _manager.DuplicateAsync(crfPageComponent);
            response.IsSuccess = response.Entity != null;
            response.EntityId = response.Entity?.Id ?? 0;
            return response;
        }



        // POST: CrfPageComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<CrfPageComponentResponse> Edit(CrfPageComponent crfPageComponent)
        {
            var response = new CrfPageComponentResponse()
            {
                EntityId = crfPageComponent.Id,
                Entity = crfPageComponent,
                IsSuccess = false
            };
            if (ModelState.IsValid)
            {
                try
                {
                    crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
                    crfPageComponent.FixByRenderType(_dataStore);
                    response.IsSuccess = await _dataStore.UpdateAsync(crfPageComponent);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    response.IsSuccess = false;
                    if (!CrfPageComponentExists(crfPageComponent.Id))
                    {
                        response.ErrorMessage = $"CrfPageComponent with id {crfPageComponent.Id} not found.";
                    }
                    else
                    {
                        response.ErrorMessage = ex.Message + "\n" + ex.StackTrace;
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = ex.Message + "\n" + ex.StackTrace;
                }
                return response;
            }

            return response;
        }

        // GET: CrfPageComponents/Delete/5
        public async Task<CrfPageComponentResponse> Delete(int id)
        {
            var response = new CrfPageComponentResponse()
            {
                EntityId = id,
                IsSuccess = false
            };

            var crfPageComponent = _dataStore.CrfPageComponents.FirstOrDefault(m => m.Id == id);
            response.Entity = crfPageComponent;
            if (crfPageComponent == null)
            {
                response.ErrorMessage = $"CrfPageComponent with id {id} not found.";
                return response;
            }

            crfPageComponent.IsDeleted = true;
            response.IsSuccess = await _dataStore.UpdateAsync(crfPageComponent);
            return response;
        }

        public async Task<CrfPageComponentResponse> UndeleteAsync(int id)
        {
            var response = new CrfPageComponentResponse()
            {
                EntityId = id,
                IsSuccess = false
            };

            var crfPageComponent = _dataStore.CrfPageComponents.FirstOrDefault(m => m.Id == id);
            response.Entity = crfPageComponent;
            if (crfPageComponent == null)
            {
                response.ErrorMessage = $"CrfPageComponent with id {id} not found.";
                return response;
            }

            crfPageComponent.IsDeleted = false;
            response.IsSuccess = await _dataStore.UpdateAsync(crfPageComponent);
            return response;
        }

        private bool CrfPageComponentExists(int id)
        {
            return _dataStore.CrfPageComponents.Any(e => e.Id == id);
        }
    }
}
