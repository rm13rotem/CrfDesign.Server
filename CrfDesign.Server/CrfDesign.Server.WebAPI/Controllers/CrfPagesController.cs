using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrfDesign.Server.WebAPI.Data;
using BuisnessLogic.Models;
using BuisnessLogic.DataContext;
using BuisnessLogic.Managers;
using CrfDesign.Server.WebAPI.Models.Filters;

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfPagesController : Controller
    {
        private readonly CrfDesignContext _context;
        private readonly CrfPageManager _crfPageManager;

        public CrfPagesController(CrfDesignContext context)
        {
            _context = context;
            _crfPageManager = new CrfPageManager(_context);
        }

        // GET: CrfPages
        public async Task<IActionResult> Index(CrfPageFilter crfPageFilter)
        {
            var pages = await _context.CrfPages.ToListAsync();
            if (!string.IsNullOrEmpty(crfPageFilter.PartialName))
                pages = pages.Where(x => x.Name.Contains(crfPageFilter.PartialName)).ToList();
            return View(pages);
        }

        // GET: CrfPages/GoToComponents/5
        public IActionResult GoToComponents(int id)
        {
            return RedirectToAction("Index", "CrfPageComponents", new CrfPageComponentFilter { CrfPageId = id });
        }

        // GET: CrfPages/RenderHTML/5
        public IActionResult RenderHTML(int id)
        {
            return RedirectToAction("Index", "RenderCrfComponent", new { id });
        }

        public IActionResult RenderReceipt(int id)
        {
            return RedirectToAction("RenderReceipt", "RenderCrfComponent", new { id });
        }

        public IActionResult RenderCSharpClass(int id)
        {
            return RedirectToAction("RenderCSharpClass", "RenderCrfComponent", new { id });
        }
        // GET: CrfPages/Duplicate/5
        public async Task<IActionResult> Duplicate(int id)
        {
            var d = await _crfPageManager.GetById(id);
            _crfPageManager.Duplicate(d);

            var filter = new CrfPageFilter() { PartialName = d.Name };
            return View("Index", filter);
        }

        // GET: CrfPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPage = await _context.CrfPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crfPage == null)
            {
                return NotFound();
            }

            return View(crfPage);
        }

        // GET: CrfPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrfPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudyId,Name,Description,CreatedAt,IsLockedForChanges,IsDeleted,ModifiedDateTime")] CrfPage crfPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crfPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crfPage);
        }

        // GET: CrfPages/Edit/5
        public async Task<IActionResult> ReturnLockedMessage(CrfPage crfPage)
        {            
            return View(crfPage);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPage = await _context.CrfPages.FindAsync(id);
            if (crfPage == null)
            {
                return NotFound();
            }

            if (crfPage.IsLockedForChanges)
                return RedirectToAction("ReturnLockedMessage", crfPage);
            return View(crfPage);
        }

        

        // POST: CrfPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CrfPage crfPage)
        {
            if (crfPage.Id == 0)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    crfPage.ModifiedDateTime = DateTime.UtcNow;
                    _context.Update(crfPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrfPageExists(crfPage.Id))
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
            return View(crfPage);
        }

        // GET: CrfPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crfPage = await _context.CrfPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crfPage == null)
            {
                return NotFound();
            }
            if (crfPage.IsLockedForChanges == true)
                return RedirectToAction("ReturnLockedMessage", crfPage);

            return View(crfPage);
        }

        // POST: CrfPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crfPage = await _context.CrfPages.FindAsync(id);
            if (crfPage.IsLockedForChanges == true)
                return RedirectToAction("ReturnLockedMessage", crfPage);
            crfPage.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: CrfPages/Delete/5
        [HttpPost, ActionName("Undelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UndeleteConfirmed(int id)
        {
            var crfPage = await _context.CrfPages.FindAsync(id);
            crfPage.IsDeleted = false;
            await _context.SaveChangesAsync();
            if (crfPage.IsLockedForChanges == true)
                return RedirectToAction("ReturnLockedMessage", crfPage);
            return RedirectToAction(nameof(Index));
        }

        private bool CrfPageExists(int id)
        {
            return _context.CrfPages.Any(e => e.Id == id);
        }
    }
}
