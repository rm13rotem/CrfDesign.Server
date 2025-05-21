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

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfOptionsController : Controller
    {
        private readonly CrfDesignContext _context;
        private readonly CrfOptionsManager _manager;

        public CrfOptionsController(CrfDesignContext context)
        {
            _context = context;
            _manager = new CrfOptionsManager(_context);
        }

        // GET: CrfOptions
        public async Task<IActionResult> Index(CrfOptionFilter crfOptionFilter)
        {
            var dbresults = await _manager.GetCrfOptionsAsync(crfOptionFilter);
            var results = dbresults.Select(x => new CrfOptionViewModel(x, _context)).ToList();
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

            return View();
        }

        // POST: CrfOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrfOption crfOption)
        {
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
        public async Task<IActionResult> Duplicate(int? id)
        {
            
            bool isSuccess = await _manager.DuplicateAsync(id);
             if (!isSuccess)
            {
                return NotFound();
            }            
            
            return RedirectToAction("Index");
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

            if (ModelState.IsValid)
            {
                try
                {
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
