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
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace CrfDesign.Server.WebAPI.Controllers
{

    [Authorize]
    public class CrfPageComponentsController : Controller
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly CrfPageComponentManager _manager;
        private readonly UserManager<Investigator> _userManager;

        public CrfPageComponentsController(UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore, IServiceScopeFactory scopedFactory)
        {
            _context = dataStore;
            _manager = new CrfPageComponentManager(_context, userManager, scopedFactory);
            _userManager = userManager;
        }

        // GET: CrfPageComponents
        public async Task<IActionResult> Index(CrfPageComponentFilter filter)
        {
            filter = _manager.GetDefaultFilter(filter);


            var items = await _manager.GetFilteredComponents(filter);

            filter.TotalLines = items.Count; // if you support paging

            ViewData["CRFPageId"] = new SelectList(_context.CrfPages.OrderBy(x => x.Id).ToList(),
                "Id", "Name", filter?.CrfPageId);

            var vm = new CrfPageComponentIndexViewModel
            {
                Filter = filter,
                Items = items
            };

            return View(vm);
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
                await _context.UpdateAsync(crfPageComponent);
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
                bool isSuccess = await _context.AddAsync(crfPageComponent);
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

            _manager.DuplicateAsync(crfPageComponent);
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
                    isSuccess = await _context.UpdateAsync(crfPageComponent);
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
                        new CrfPageComponentFilter { CrfPageId = crfPageComponent.CRFPageId });
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
            _context.DeleteAsync<CrfPageComponent>(id);
            var crfPageComponent = _context.CrfPageComponents
                .FirstOrDefault(x => x.Id == id);
            crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
            _context.UpdateAsync(crfPageComponent);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Refresh(int id)
        {
            var crfPageComponent = _context.CrfPageComponents
                   .FirstOrDefault(x => x.Id == id);
            crfPageComponent.ModifiedDateTime = DateTime.UtcNow;
            _context.UpdateAsync(crfPageComponent);
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
