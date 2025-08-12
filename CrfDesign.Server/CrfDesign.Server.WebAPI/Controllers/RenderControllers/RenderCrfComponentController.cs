using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.AdminManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers.RenderControllers
{
    public class RenderCrfComponentController : Controller
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly IRuntimeEnvironment _env;

        public RenderCrfComponentController(IInMemoryCrfDataStore dataStore, IRuntimeEnvironment env)
        {
            _context = dataStore;
            _env = env;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (_env.Current == "Development")
            {
                // show detailed logs, enable debug tools, etc.
                try
                {
                    return RenderHtmlPageByCrfId(id);
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message + "\n" + ex.StackTrace);
                }
            }
            else
            {
                // production behavior
                return RenderHtmlPageByCrfId(id);
            }
        }

        private IActionResult RenderHtmlPageByCrfId(int? id)
        {
            if (id == null)
                return RedirectToAction($"{nameof(Index)}", "CrfPages");

            RenderCrfPageViewModel model = GetRenderViewModel(id);

            if (model == null)
                return NotFound();

            foreach (var item in model.CrfPageComponent.Where(x => x.QuestionType == "SingleChoice"))
            {
                var Options = _context.CrfOptions.Where(x => x.CrfOptionCategoryId == item.CategoryId).ToList();
                Options.Add(new CrfOption() { Id = 0, Name = "--Select--" });
                Options = Options.OrderBy(x => x.Id).ToList();
                ViewData[item.Name] = new SelectList(Options, "Id", "Name", 0);
            }

            return View(model);
        }

        private RenderCrfPageViewModel GetRenderViewModel(int? id)
        {
            if (id == null) return null;

            var crfPage = _context.CrfPages.FirstOrDefault(x=>x.Id == id);
            if (crfPage == null) return null;

            // Preload lookup data into dictionaries for O(1) lookup
            var questionTypesDict = _context.QuestionTypes
                .ToDictionary(q => q.Id, q => q.Name);

            var categoriesDict = _context.CrfOptionCategories
                .ToDictionary(c => c.Id, c => c.Name);

            var optionsLookup = _context.CrfOptions.ToList()
                .GroupBy(o => o.CrfOptionCategoryId)
                .ToDictionary(g => g.Key, g => g.Select(o => o.Name).ToList());

            var crfPageComponents = _context.CrfPageComponents
                .Where(x => x.CRFPageId == id && !x.IsDeleted)
                .OrderBy(x => x.Id)
                .ToList() // Switch to in-memory for projection
                .Select(component =>
                {
                    // Get QuestionType name safely
                    questionTypesDict.TryGetValue(component.QuestionTypeId, out string questionType);

                    // Get category name and options safely
                    string categoryName = null;
                    List<string> categoryOptions = new();

                    if (component.CategoryId.HasValue)
                    {
                        categoriesDict.TryGetValue(component.CategoryId.Value, out categoryName);
                        optionsLookup.TryGetValue(component.CategoryId.Value, out categoryOptions);
                    }

                    return new CrfPageComponentViewModel(
                        component,
                        crfPage.Name,
                        questionType,
                        categoryName,
                        categoryOptions ?? new List<string>()
                    );
                })
                .ToList();

            if (!crfPageComponents.Any()) return null;

            return new RenderCrfPageViewModel(crfPage, crfPageComponents);
        }


        public IActionResult RenderReceipt(int id)
        {
            if (id == null)
                return RedirectToAction($"{nameof(Index)}", "CrfPages");

            RenderCrfPageViewModel model = GetRenderViewModel(id);

            if (model == null)
                return NotFound();
            return View(model);
        }

        public IActionResult RenderCSharpClass(int id)
        {
            if (id == null)
                return RedirectToAction($"{nameof(Index)}", "CrfPages");

            RenderCrfPageViewModel model = GetRenderViewModel(id);

            if (model == null)
                return NotFound();
            return View(model);
        }

    }
}
