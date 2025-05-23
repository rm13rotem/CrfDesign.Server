using BuisnessLogic.DataContext;
using CrfDesign.Server.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction($"{nameof(Index)}", "CrfPages");

            var CrfPage = _context.CrfPages.Find(id);
            var CrfPageComponent = _context.CrfPageComponents.Where(x => x.CRFPageId == id).ToList();
            if (CrfPage == null || CrfPageComponent.Count == 0)
                return NotFound();
            var model = new RenderCrfPageViewModel(CrfPage, CrfPageComponent);
            return View(model);
        }
    }
}
