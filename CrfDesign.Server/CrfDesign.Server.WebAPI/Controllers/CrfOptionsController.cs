using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrfDesign.Server.WebAPI.Data;
using CrfDesign.Server.WebAPI.Models;

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfOptionsController : Controller
    {
        private readonly CrfDesignContext _context;

        public CrfOptionsController(CrfDesignContext context)
        {
            _context = context;
        }

        // GET: CrfOptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CrfOptions.ToListAsync());
        }

        // GET: CrfOptions/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: CrfOptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrfOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsDeleted,ModifiedDateTime,CrfQuestionId")] CrfOption crfOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crfOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crfOption);
        }

        // GET: CrfOptions/Edit/5
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
            return View(crfOption);
        }

        // POST: CrfOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,ModifiedDateTime,CrfQuestionId")] CrfOption crfOption)
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
            _context.CrfOptions.Remove(crfOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrfOptionExists(int id)
        {
            return _context.CrfOptions.Any(e => e.Id == id);
        }
    }
}
