using BuisnessLogic.Filters;
using BuisnessLogic.Models;
using CrfDesign.Server.WebAPI.Models.ApiControllerResponses;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers.ApiControllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CrfOptionCategoryApiController : ControllerBase
    {
        private readonly CrfOptionCategoryManager _manager;

        public CrfOptionCategoryApiController(CrfOptionCategoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<CrfOptionCategory>> Index(CrfOptionCategoriesFilter filter)
        {
            var result = await _manager.GetAllAsync(filter);
            if (result == null)
                return new List<CrfOptionCategory>();

            return result;
        }

        public async Task<CrfOptionCategory> Details(int? id)
        {
            var result = _manager.GetDetails(id);
            return result;
        }


        [HttpPost]
        public async Task<CrfOptionCategoryResponse> Create(CrfOptionCategory crfOptionCategory)
        {
            var createResult = new CrfOptionCategoryResponse
            {
                Category = crfOptionCategory,
                EntityId = crfOptionCategory.Id,
            };
            if (!ModelState.IsValid)
            {
                createResult.IsSuccess = false;
                createResult.ErrorMessage = $"Model  is Invalid \n{ModelState.ValidationState}";
                return createResult;
            }

            var success = await _manager.CreateAsync(crfOptionCategory);
            createResult.IsSuccess = success;
            if (!success) createResult.ErrorMessage = $"Save to database of {nameof(CrfOptionCategory)} failed";
            return createResult;
        }

        

        [HttpPut]
        public async Task<CrfOptionCategoryResponse> Edit(CrfOptionCategory crfOptionCategory)
        {
            var response = new CrfOptionCategoryResponse
            {
                Category = crfOptionCategory,
                EntityId = crfOptionCategory.Id,
            };
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Model  is Invalid \n{ModelState.ValidationState}";
                return response;
            }
            var success = await _manager.EditAsync(crfOptionCategory);
            response.IsSuccess = success;
            if (!success) response.ErrorMessage = $"Save of update to database of {nameof(CrfOptionCategory)} (id {crfOptionCategory?.Id}) failed";
            return response;
        }

        [HttpDelete]
        public async Task<CrfOptionCategoryResponse> Delete(int? id)
        {
            var result = _manager.GetDelete(id);
            var response = new CrfOptionCategoryResponse
            {
                Category = result,
                EntityId = id ?? 0,
                IsSuccess = result != null,
            };
            return response;
        }

        [HttpGet]
        [HttpPatch]
        public async Task<CrfOptionCategoryResponse> Undelete(int? id)
        {
            var result = await _manager.UnDelete(id ?? 0);
            var response = new CrfOptionCategoryResponse
            {
                EntityId = id ?? 0,
                IsSuccess = result,
            };
            return response;
        }



    }
}
