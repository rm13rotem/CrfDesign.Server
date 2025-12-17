using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Models.Managers;
using BuisnessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrfDesign.Server.WebAPI.Models;
using BuisnessLogic.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using BuisnessLogic.Interfaces.Managers;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace CrfDesign.Server.WebAPI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CrfOptionsController : Controller
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly ICrfOptionsManager _manager;
        private readonly CrfPageManager _pageManager;
        private readonly UserManager<Investigator> _userManager;
        private readonly ILogger<CrfOptionsController> _logger;

        public CrfOptionsController(
            UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore,
            IServiceScopeFactory scopeFactory,
            IHttpContextAccessor httpContextAccessor,
            ICrfOptionsManager manager,
            ILogger<CrfOptionsController> logger)
        {
            _context = dataStore;
            _logger = logger;
            _manager = manager;
            _userManager = userManager;
            _pageManager = new CrfPageManager(dataStore, userManager, scopeFactory, httpContextAccessor);

        }

        // GET: CrfOptions
        public async Task<IActionResult> Index(CrfOptionFilter filter)
        {
            if (filter == null) filter = new CrfOptionFilter();//
            filter.TotalLines = _context.CrfOptions.Count();
            var dbresults = _manager.GetCrfOptionsAsync(filter);

            var results = dbresults.Select(x => new CrfOptionViewModel(x, _context)).ToList();
            foreach (var option in results)
            {
                if (!string.IsNullOrEmpty(option.LastUpdatorUserId))
                {
                    var user = await _userManager.FindByIdAsync(option.LastUpdatorUserId);
                    if (user != null)
                        option.LastUpdatorUserId = $"{user.FirstName} {user.LastName}";
                    else option.LastUpdatorUserId = "Unknown";
                }
            }
            var Options = _context.CrfOptionCategories.ToList();
            Options.Add(new CrfOptionCategory() { Id = 0, Name = "All" });
            ViewData["CategoryId"] = new SelectList(Options, "Id", "Name", filter.CategoryId);
            ViewBag.filter = filter;
            return View(results);
        }

        // GET: CrfOptions/Details/5
        public IActionResult Details(int id)
        {
            var crfOption = _manager.GetById(id);
            if (crfOption == null)
            {
                return NotFound();
            }

            return View(crfOption);
        }

        // GET: CrfOptions/Create
        public IActionResult Create()
        {
            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name");
            var m = new CrfOption();
            m.ModifiedDateTime = DateTime.UtcNow;
            return View(m);
        }

        // POST: CrfOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrfOption crfOption)
        {
            CrfPage crfPage = GetLockedCrfByOptionId(crfOption.Id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }
            if (ModelState.IsValid)
            {
                bool isSuccess = await _context.AddAsync(crfOption);
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name", crfOption.CrfOptionCategoryId);

            return View(crfOption);
        }



        // GET: CrfOptions/Edit/5
        public async Task<IActionResult> Duplicate(int id, CrfOptionFilter filter)
        {
            CrfPage crfPage = GetLockedCrfByOptionId(id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }
            bool isSuccess = await _manager.DuplicateAsync(id);
            if (!isSuccess)
            {
                return NotFound();
            }

            return RedirectToAction("Index", filter);
        }

        // GET: CrfOptions/ReturnLockedMessage/5
        public IActionResult ReturnLockedMessage(CrfPage crfPage)
        {
            return View(crfPage);
        }


        private CrfPage GetLockedCrfByOptionId(int? id)
        {

            CrfOption crfOption = _context.CrfOptions.Find(x => x.Id == id);
            if (crfOption == null) return null;

            var category = _context.CrfOptionCategories.Find(x => x.Id == crfOption.CrfOptionCategoryId);
            if (category == null) return null;

            return GetLockedCrfByCategory(category.Id);
        }

        private CrfPage GetLockedCrfByCategory(int crfOptionCategoryId)
        {

            var components = _context.CrfPageComponents
                .Where(x => x.CategoryId == crfOptionCategoryId).ToList();
            //maybe many components
            if (components.Count == 0)
                return null;

            var componentCrfPageIds = components.Select(x => x.CRFPageId).Distinct().ToList();
            var crfPages = _context.CrfPages
                .Where(x => x.IsLockedForChanges)
                .ToList().Where(x => componentCrfPageIds.Any(y => y == x.Id)).ToList();

            // may be many CRF pages with this specific option drop down in them
            CrfPage lockedCRF = crfPages.FirstOrDefault(x => x.IsLockedForChanges == true);
            return lockedCRF;
        }


        // GET: CrfOptions/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfOption = _context.CrfOptions.FirstOrDefault(x => x.Id == id);
            if (crfOption == null)
            {
                return NotFound();
            }
            CrfPage crfPage = GetLockedCrfByOptionId(id);
            if (_pageManager.IsPageLockedForChanges(crfPage.Id))
                return RedirectToAction("ReturnLockedMessage", crfPage);

            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name", crfOption.CrfOptionCategoryId);

            return View(crfOption);
        }

        // POST: CrfOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CrfOption crfOption)
        {
            if (id != crfOption.Id)
            {
                return NotFound();
            }
            CrfPage crfPage = GetLockedCrfByOptionId(id);
            if (_pageManager.IsPageLockedForChanges(crfPage.Id))
                return RedirectToAction("ReturnLockedMessage", crfPage);

            bool isSuccess;
            if (ModelState.IsValid)
            {
                try
                {
                    crfOption.ModifiedDateTime = DateTime.UtcNow;
                    isSuccess = await _context.UpdateAsync(crfOption);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrfOptionExists(crfOption.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name", crfOption.CrfOptionCategoryId);

            return View(crfOption);
        }

        // GET: CrfOptions/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CrfPage crfPage = GetLockedCrfByOptionId(id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }

            var crfOption = _context.CrfOptions.FirstOrDefault(m => m.Id == id);
            if (crfOption == null)
            {
                return NotFound();
            }

            return View(crfOption);
        }

        // POST: CrfOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isSuccess = await _context.DeleteAsync<CrfOption>(id);
            if (!isSuccess)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UndeleteAsync(int id)
        {
            bool isSuccess = await _context.UndeleteAsync<CrfOption>(id);
            if (!isSuccess)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        private bool CrfOptionExists(int id)
        {
            return _context.CrfOptions.Any(e => e.Id == id);
        }
    }
}
