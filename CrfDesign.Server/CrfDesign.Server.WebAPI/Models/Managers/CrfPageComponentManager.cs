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
    public class CrfPageComponentManager
    {
        private readonly IInMemoryCrfDataStore _dataStore;
        private readonly UserManager<Investigator> _userManager;
        private readonly IServiceScopeFactory _scopeFactory;
        public CrfPageComponentManager(IInMemoryCrfDataStore dataStore,
            UserManager<Investigator> userManager,
            IServiceScopeFactory scopeFactory)
        {
            _dataStore = dataStore;
            _userManager = userManager;
            _scopeFactory = scopeFactory;
        }

        public bool Lock(CrfPageComponent component)
        {
            _dataStore.Refresh();
            if (!component.IsLockedForChanges)
            {
                component.IsLockedForChanges = true;
                if (!_dataStore.Update(component))
                    return false;
            }
            var SingleChoiceQuestionType = _dataStore.QuestionTypes.
                FirstOrDefault(x => x.Name.ToLower() == "SingleChoice".ToLower());
            if (SingleChoiceQuestionType == null)
                return false;

            if (component.QuestionTypeId == SingleChoiceQuestionType.Id)
            {
                bool isSuccess = LockCategory(component.CategoryId);
                if (!isSuccess)
                    return false;

                isSuccess = LockOptions(component.CategoryId);
                if (!isSuccess)
                    return false;

            }
            return true;
        }

        private bool LockOptions(int? categoryId)
        {
            var options = _dataStore.CrfOptions.
                Where(x => x.CrfOptionCategoryId == categoryId).ToList();
            foreach (var option in options)
            {
                option.IsLockedForChanges = true;
                if (!_dataStore.Update(option))
                    return false;
            }
            return true;
        }

        private bool LockCategory(int? categoryId)
        {
            var category = _dataStore.CrfOptionCategories.
                    FirstOrDefault(x => x.Id == categoryId);
            if (category == null)
                return false;

            category.IsLockedForChanges = true;
            if (!_dataStore.Update(category))
                return false;

            return true;
        }
    }
}
