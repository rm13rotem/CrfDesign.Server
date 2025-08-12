using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrfDesign.Server.WebAPI.Models;
using BuisnessLogic.Models;
using BuisnessLogic.DataContext;
using BuisnessLogic.Models.Managers;
using CrfDesign.Server.WebAPI.Models.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BuisnessLogic.Repositories;

namespace CrfDesign.Server.WebAPI.Controllers
{

    [Authorize]
    public class CrfPageComponentsController : Controller
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly Manager<CrfPageComponent> _manager;
        private readonly UserManager<Investigator> _userManager;

        public CrfPageComponentsController(UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore)
        {
            _context = dataStore;
            _manager = new Manager<CrfPageComponent>(_context);
            _userManager = userManager;
        }

        // GET: CrfPageComponents
        public async Task<IActionResult> Index(CrfPageComponentFilter filter)
        {
            List<CrfPageComponent> dblines = null;
            if (filter.CrfPageId > 0)
                dblines = _context.CrfPageComponents
                    .Where(x => x.CRFPageId == filter.CrfPageId).ToList();
            else dblines = _context.CrfPageComponents;

            if (!string.IsNullOrEmpty(filter.PartialName))
                dblines = dblines
                    .Where(x => x.Name.Contains(filter.PartialName) == true)
                    .ToList();

            List<CrfPageComponentViewModel> uiLines = dblines.Select(crfComponent =>
            {
                var crfPageName = _context.CrfPages.FirstOrDefault(x => x.Id == crfComponent.CRFPageId)?.Name ?? "Not Selected";
                var questionType = _context.QuestionTypes.FirstOrDefault(x => x.Id == crfComponent.QuestionTypeId)?.Name ?? "Not Selected";
                var category = _context.CrfOptionCategories.FirstOrDefault(x => x.Id == crfComponent.CategoryId);
                string categoryName = string.Empty;
                List<string> categoryOptions = new();
                if (category != null)
                {
                    categoryName = category.Name;
                    categoryOptions = _context.CrfOptions.
                    Where(x => x.CrfOptionCategoryId == crfComponent.CategoryId)
                    .Select(x => x.Name).ToList();
                    if (!categoryOptions.Any())
                        categoryOptions.Add("**" + categoryName);
                }

                return new CrfPageComponentViewModel(crfComponent,
                     crfPageName, questionType, categoryName, categoryOptions);
            }).ToList();

            foreach (var component in uiLines)
            {
                if (!string.IsNullOrEmpty(component.LastUpdatorUserId))
                {
                    var user = await _userManager.FindByIdAsync(component.LastUpdatorUserId);
                    if (user != null)
                        component.LastUpdatorUserId = $"{user.FirstName} {user.LastName}";
                    else component.LastUpdatorUserId = "Unknown";
                }
            }

            var crfPages = _context.CrfPages.ToList();
            for (int i = 0; i < crfPages.Count; i++)
            {
                crfPages[i].Name = string.Format("{0} - {1}",
                    crfPages[i].Id, crfPages[i].Name);
            }
            crfPages.Add(new CrfPage() { Id = 0, Name = "All" });
            crfPages = crfPages.OrderBy(x => x.Id).ToList();

            ViewData["CRFPageId"] = new SelectList(crfPages, "Id", "Name", filter.CrfPageId);
            return View(uiLines);
        }

        // GET: CrfPageComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPageComponent = _context.CrfPageComponents
                .FirstOrDefault(m => m.Id == id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }
            // else
            if (crfPageComponent.ModifiedDateTime < new DateTime(1900, 1, 1))
            {
                crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
                _context.Update(crfPageComponent);
            }
            var Options = _context.CrfOptionCategories.ToList();
            Options.Add(new CrfOptionCategory() { Id = 0, Name = "None" });
            ViewData["CategoryId"] = new SelectList(Options, "Id", "Name", crfPageComponent.CategoryId);
            return View(crfPageComponent);
        }

        // GET: CrfPageComponents/Create
        public IActionResult Create()
        {
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name");
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name");
            var Options = _context.CrfOptionCategories.ToList();
            Options.Add(new CrfOptionCategory() { Id = 0, Name = "None" });
            ViewData["CategoryId"] = new SelectList(Options, "Id", "Name");
            return View();
        }

        // POST: CrfPageComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrfPageComponent crfPageComponent)
        {
            if (ModelState.IsValid)
            {
                crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
                bool isSuccess = _context.Add(crfPageComponent);
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name", crfPageComponent.CRFPageId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name", crfPageComponent.QuestionTypeId);
            var Options = _context.CrfOptionCategories.ToList();
            Options.Add(new CrfOptionCategory() { Id = 0, Name = "None" });
            ViewData["CategoryId"] = new SelectList(Options, "Id", "Name", crfPageComponent.CategoryId);
            return View(crfPageComponent);
        }
        // GET: CrfPageComponents/Duplicate/5
        public IActionResult Duplicate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var crfPageComponent = _context.CrfPageComponents.FirstOrDefault(x => x.Id == id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }

            _manager.Duplicate(crfPageComponent);
            var crfPageFilter = new CrfPageComponentFilter() { PartialName = crfPageComponent.Name };
            return RedirectToAction("Index", crfPageFilter);
        }

        // GET: CrfPageComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPageComponent = _context.CrfPageComponents.FirstOrDefault(x => x.Id == id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }

            //else
            crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name", crfPageComponent.CRFPageId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name", crfPageComponent.QuestionTypeId);
            var Options = _context.CrfOptionCategories.ToList();
            Options.Add(new CrfOptionCategory() { Id = 0, Name = "None" });
            ViewData["CategoryId"] = new SelectList(Options, "Id", "Name");
            return View(crfPageComponent);
        }

        // POST: CrfPageComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CrfPageComponent crfPageComponent)
        {
            if (id != crfPageComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isSuccess;
                try
                {
                    crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
                    crfPageComponent.FixByRenderType(_context);
                    isSuccess = _context.Update(crfPageComponent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    isSuccess = false;
                    if (!CrfPageComponentExists(crfPageComponent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (isSuccess)
                    return RedirectToAction(nameof(Index),
                        new CrfPageComponentFilter
                        {
                            CrfPageId = crfPageComponent.CRFPageId
                        });
            }
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name", crfPageComponent.CRFPageId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name", crfPageComponent.QuestionTypeId);
            var Options = _context.CrfOptionCategories.ToList();
            Options.Add(new CrfOptionCategory() { Id = 0, Name = "None" });
            ViewData["CategoryId"] = new SelectList(Options, "Id", "Name", crfPageComponent.CategoryId);
            return View(crfPageComponent);
        }

        // GET: CrfPageComponents/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPageComponent = _context.CrfPageComponents.FirstOrDefault(m => m.Id == id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }

            return View(crfPageComponent);
        }

        // POST: CrfPageComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Delete<CrfPageComponent>(id);
            var crfPageComponent = _context.CrfPageComponents
                .FirstOrDefault(x => x.Id == id);
            crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
            _context.Update(crfPageComponent);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Refresh(int id)
        {
            var crfPageComponent = _context.CrfPageComponents
                   .FirstOrDefault(x => x.Id == id);
            crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
            _context.Update(crfPageComponent);
            var backToPageFilter = new CrfPageComponentFilter()
            {
                CrfPageId = crfPageComponent.CRFPageId
            };
            return RedirectToAction(nameof(Index), backToPageFilter);
        }
        private bool CrfPageComponentExists(int id)
        {
            return _context.CrfPageComponents.Any(e => e.Id == id);
        }
    }
}
