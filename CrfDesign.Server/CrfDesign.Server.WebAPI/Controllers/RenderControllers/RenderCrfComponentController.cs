using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using CrfDesign.Server.WebAPI.Models;
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
        private readonly CrfDesignContext _context;

        public RenderCrfComponentController(CrfDesignContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
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
            var CrfPage = _context.CrfPages.Find(id);
            if (CrfPage == null) return null;
            var CrfPageComponents = _context.CrfPageComponents
                .Where(x => x.CRFPageId == id)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Id)
                .Select(x => new CrfPageComponentViewModel(x, _context))
                .ToList();
            if (CrfPageComponents.Any() == false)
                return null;
            var model = new RenderCrfPageViewModel(CrfPage, CrfPageComponents);
            return model;
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
