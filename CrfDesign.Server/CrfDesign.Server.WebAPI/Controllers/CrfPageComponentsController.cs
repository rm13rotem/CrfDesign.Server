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

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfPageComponentsController : Controller
    {
        private readonly CrfDesignContext _context;
        private readonly Manager<CrfPageComponent> _manager;

        public CrfPageComponentsController(CrfDesignContext context)
        {
            _context = context;
            _manager = new Manager<CrfPageComponent>(_context);
        }

        // GET: CrfPageComponents
        public async Task<IActionResult> Index()
        {
            var dblines = await _context.CrfPageComponents.ToListAsync();
            var uiLines = dblines.Select(crfComponent => 
                        new CrfPageComponentViewModel(crfComponent, _context))
                .ToList();
            return View(uiLines);
        }

        // GET: CrfPageComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPageComponent = await _context.CrfPageComponents
                .Include(c => c.CrfPage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }

            return View(crfPageComponent);
        }

        // GET: CrfPageComponents/Create
        public IActionResult Create()
        {
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name");
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name");
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
                _context.Add(crfPageComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name", crfPageComponent.CRFPageId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name", crfPageComponent.QuestionTypeId);
            return View(crfPageComponent);
        }
        // GET: CrfPageComponents/Duplicate/5
        public async Task<IActionResult> Duplicate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var crfPageComponent = await _context.CrfPageComponents.FindAsync(id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }

            _manager.Duplicate(crfPageComponent);
            return RedirectToAction("Index");
        }

            // GET: CrfPageComponents/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPageComponent = await _context.CrfPageComponents.FindAsync(id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name", crfPageComponent.CRFPageId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name", crfPageComponent.QuestionTypeId);
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
                try
                {
                    crfPageComponent.FixByRenderType(_context);
                    _context.Update(crfPageComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrfPageComponentExists(crfPageComponent.Id))
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
            ViewData["CRFPageId"] = new SelectList(_context.CrfPages, "Id", "Name", crfPageComponent.CRFPageId);
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "Name", crfPageComponent.QuestionTypeId);
            return View(crfPageComponent);
        }

        // GET: CrfPageComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPageComponent = await _context.CrfPageComponents
                .Include(c => c.CrfPage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crfPageComponent == null)
            {
                return NotFound();
            }

            return View(crfPageComponent);
        }

        // POST: CrfPageComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crfPageComponent = await _context.CrfPageComponents.FindAsync(id);
            _context.CrfPageComponents.Remove(crfPageComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrfPageComponentExists(int id)
        {
            return _context.CrfPageComponents.Any(e => e.Id == id);
        }
    }
}
