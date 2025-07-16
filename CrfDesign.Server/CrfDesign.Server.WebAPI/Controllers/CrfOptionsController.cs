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

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfOptionsController : Controller
    {
        private readonly CrfDesignContext _context;
        private readonly CrfOptionsManager _manager;
        private readonly UserManager<Investigator> _userManager;


        public CrfOptionsController(UserManager<Investigator> userManager,
            CrfDesignContext context)
        {
            _context = context;
            _manager = new CrfOptionsManager(_context);
            _userManager = userManager;
        }

        // GET: CrfOptions
        public async Task<IActionResult> Index(CrfOptionFilter filter)
        {
            if (filter == null) filter = new CrfOptionFilter();//
            filter.TotalLines = _context.CrfOptions.Count();
            var dbresults = await _manager.GetCrfOptionsAsync(filter);
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfOption = await _manager.GetById((int)id);
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
            CrfPage crfPage = GetLockedCrfByCategory(crfOption.CrfOptionCategoryId);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }
            if (ModelState.IsValid)
            {
                _context.Add(crfOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name", crfOption.CrfOptionCategoryId);

            return View(crfOption);
        }



        // GET: CrfOptions/Edit/5
        public async Task<IActionResult> Duplicate(int? id, CrfOptionFilter filter)
        {
            CrfPage crfPage = GetLockedCrf(id);
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

        // GET: CrfOptions/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfOption = await _context.CrfOptions.FindAsync(id);
            if (crfOption == null)
            {
                return NotFound();
            }
            CrfPage crfPage = GetLockedCrf(id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }
            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name", crfOption.CrfOptionCategoryId);

            return View(crfOption);
        }

        private CrfPage GetLockedCrf(int? id)
        {

            CrfOption crfOption = _context.CrfOptions.Find(id);
            if (crfOption == null) return null;

            var category = _context.CrfOptionCategories.Find(crfOption.CrfOptionCategoryId);
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
            CrfPage crfPage = GetLockedCrf(id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }
            crfPage = GetLockedCrfByCategory(crfOption.CrfOptionCategoryId);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    crfOption.ModifiedDateTime = DateTime.UtcNow;
                    _context.Update(crfOption);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrfOptionCategoryId"] = new SelectList(_context.CrfOptionCategories, "Id", "Name", crfOption.CrfOptionCategoryId);

            return View(crfOption);
        }

        // GET: CrfOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CrfPage crfPage = GetLockedCrf(id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }

            var crfOption = await _context.CrfOptions
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var crfOption = await _context.CrfOptions.FindAsync(id);
            CrfPage crfPage = GetLockedCrf(id);
            if (crfPage != null)
            {
                return RedirectToAction("ReturnLockedMessage", crfPage);
            }
            crfOption.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrfOptionExists(int id)
        {
            return _context.CrfOptions.Any(e => e.Id == id);
        }
    }
}
