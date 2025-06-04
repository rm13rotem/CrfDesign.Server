using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using BuisnessLogic.Filters;

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfOptionCategoriesController : Controller
    {
        private readonly CrfDesignContext _context;

        public CrfOptionCategoriesController(CrfDesignContext context)
        {
            _context = context;
        }

        // GET: CrfOptionCategories
        public async Task<IActionResult> Index(CrfOptionCategoriesFilter filter)
        {
            filter.TotalLines = _context.CrfOptionCategories.Count();
            var dbLines = await _context.CrfOptionCategories.OrderBy(x => x.Id).Skip(filter.Page - 1).Take(filter.NLines).ToListAsync();
            if (dbLines.Count == 0)
                dbLines = await _context.CrfOptionCategories.ToListAsync();
            else ViewBag.filter = filter;
            return View(dbLines);
        }

        // GET: CrfOptionCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfOptionCategory = await _context.CrfOptionCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crfOptionCategory == null)
            {
                return NotFound();
            }

            return View(crfOptionCategory);
        }

        // GET: CrfOptionCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrfOptionCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsDeleted,ModifiedDateTime")] CrfOptionCategory crfOptionCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crfOptionCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crfOptionCategory);
        }

        // GET: CrfOptionCategories/Duplicate/5
        public async Task<IActionResult> Duplicate(int? id, CrfOptionCategoriesFilter filter)
        {

            return RedirectToAction($"{nameof(Index)}", filter);
        }

        // GET: CrfOptionCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfOptionCategory = await _context.CrfOptionCategories.FindAsync(id);
            if (crfOptionCategory == null)
            {
                return NotFound();
            }
            return View(crfOptionCategory);
        }

        // POST: CrfOptionCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,ModifiedDateTime")] CrfOptionCategory crfOptionCategory)
        {
            if (id != crfOptionCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crfOptionCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrfOptionCategoryExists(crfOptionCategory.Id))
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
            return View(crfOptionCategory);
        }

        // GET: CrfOptionCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfOptionCategory = await _context.CrfOptionCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crfOptionCategory == null)
            {
                return NotFound();
            }

            return View(crfOptionCategory);
        }

        // POST: CrfOptionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crfOptionCategory = await _context.CrfOptionCategories.FindAsync(id);
            _context.CrfOptionCategories.Remove(crfOptionCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrfOptionCategoryExists(int id)
        {
            return _context.CrfOptionCategories.Any(e => e.Id == id);
        }
    }
}
