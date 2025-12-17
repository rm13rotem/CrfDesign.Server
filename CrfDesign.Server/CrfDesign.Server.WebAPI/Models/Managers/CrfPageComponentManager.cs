using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CrfPageComponentManager : Manager<CrfPageComponent>
    {
        private readonly IInMemoryCrfDataStore _dataStore;
        private readonly UserManager<Investigator> _userManager;
        private readonly IServiceScopeFactory _scopeFactory;
        public CrfPageComponentManager(IInMemoryCrfDataStore dataStore,
            UserManager<Investigator> userManager,
            IServiceScopeFactory scopeFactory) : base(dataStore)
        {
            _dataStore = dataStore;
            _userManager = userManager;
            _scopeFactory = scopeFactory;
        }

        public async Task<bool> LockAsync(CrfPageComponent component)
        {
            _dataStore.LoadData("CrfPageComponent");

            if (!component.IsLockedForChanges)
            {
                component.IsLockedForChanges = true;
                if (!await _dataStore.UpdateAsync(component))
                    return false;
            }
            var SingleChoiceQuestionType = _dataStore.QuestionTypes.
                FirstOrDefault(x => x.Name.ToLower() == "SingleChoice".ToLower());
            if (SingleChoiceQuestionType == null)
                return false;

            if (component.QuestionTypeId == SingleChoiceQuestionType.Id)
            {
                bool isSuccess = await LockCategoryAsync(component.CategoryId);
                if (!isSuccess)
                    return false;

                isSuccess = await LockOptionsAsync(component.CategoryId);
                if (!isSuccess)
                    return false;

            }
            return true;
        }

        public CrfPageComponentFilter GetDefaultFilter(CrfPageComponentFilter filter)
        {
            if (filter == null)
            {
                filter = new CrfPageComponentFilter();
            }
            if (filter.NLines == 0)
                filter.NLines = 20;
            // if filter.page = 0 --> ok
            if (filter.PartialName == null)
                filter.PartialName = string.Empty;
            return filter;
        }

        private async Task<bool> LockOptionsAsync(int? categoryId)
        {
            var options = _dataStore.CrfOptions.
                Where(x => x.CrfOptionCategoryId == categoryId).ToList();
            foreach (var option in options)
            {
                option.IsLockedForChanges = true;
                if (!await _dataStore.UpdateAsync(option))
                    return false;
            }
            return true;
        }

        private async Task<bool> LockCategoryAsync(int? categoryId)
        {
            var category = _dataStore.CrfOptionCategories.
                    FirstOrDefault(x => x.Id == categoryId);
            if (category == null)
                return false;

            category.IsLockedForChanges = true;
            if (!await _dataStore.UpdateAsync(category))
                return false;

            return true;
        }

        public async Task<List<CrfPageComponentViewModel>> GetFilteredComponents(
    CrfPageComponentFilter filter)
        {
            filter ??= new CrfPageComponentFilter();

            // Base query
            var query = _dataStore.CrfPageComponents;

            // Filter by page
            if (filter.CrfPageId > 0)
            {
                query = query.Where(x => x.CRFPageId == filter.CrfPageId).ToList();
            }

            // Filter by name
            if (!string.IsNullOrEmpty(filter.PartialName))
            {
                query = query.Where(x => x.Name.Contains(filter.PartialName)).ToList();
            }

            // Total before paging
            var totalCount = query.Count();

            // Paging (optional – safe even if you don't use it yet)
            query = query
                .Skip((filter.Page-1) * filter.NLines)
                .Take(filter.NLines).ToList();

            // Load lookup tables ONCE
            var pages = _dataStore.CrfPages.ToDictionary(x => x.Id, x => x.Name);
            var questionTypes = _dataStore.QuestionTypes.ToDictionary(x => x.Id, x => x.Name);
            var categories = _dataStore.CrfOptionCategories.ToDictionary(x => x.Id);
            var options = _dataStore.CrfOptions
                .GroupBy(x => x.CrfOptionCategoryId)
                .ToDictionary(g => g.Key, g => g.Select(o => o.Name).ToList());

            var result = new List<CrfPageComponentViewModel>();

            foreach (var component in query.ToList())
            {
                pages.TryGetValue(component.CRFPageId, out var pageName);
                questionTypes.TryGetValue(component.QuestionTypeId, out var questionTypeName);

                string categoryName = string.Empty;
                List<string> categoryOptions = new();

                if (component.CategoryId != null &&
                    categories.TryGetValue(component.CategoryId.Value, out var category))
                {
                    categoryName = category.Name;

                    if (options.TryGetValue(component.CategoryId.Value, out var opts) && opts.Any())
                        categoryOptions = opts;
                    else
                        categoryOptions.Add("**" + categoryName);
                }

                var vm = new CrfPageComponentViewModel(
                    component,
                    pageName ?? "Not Selected",
                    questionTypeName ?? "Not Selected",
                    categoryName,
                    categoryOptions);

                // Resolve user name
                if (!string.IsNullOrEmpty(vm.LastUpdatorUserId))
                {
                    var user = await _userManager.FindByIdAsync(vm.LastUpdatorUserId);
                    vm.LastUpdatorUserId = user != null
                        ? $"{user.FirstName} {user.LastName}"
                        : "Unknown";
                }

                result.Add(vm);
            }

            return result;
        }

    }
}
