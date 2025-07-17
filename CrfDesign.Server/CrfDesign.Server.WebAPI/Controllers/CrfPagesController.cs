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
using CrfDesign.Server.WebAPI.Models.Filters;
using Microsoft.AspNetCore.Identity;
using CrfDesign.Server.WebAPI.Models.Managers;
using CrfDesign.Server.WebAPI.Models;

namespace CrfDesign.Server.WebAPI.Controllers
{
    public class CrfPagesController : Controller
    {
        private readonly CrfPageManager _manager;

        public CrfPagesController(UserManager<Investigator> userManager, CrfDesignContext context)
        {
            _manager = new CrfPageManager(context, userManager);
        }

        public async Task<IActionResult> Index(CrfPageFilter filter)
        {
            var pages = await _manager.GetFilteredPagesAsync(filter);
            return View(pages);
        }

        public IActionResult GoToComponents(int id)
            => RedirectToAction("Index", "CrfPageComponents", new CrfPageComponentFilter { CrfPageId = id });

        public IActionResult RenderHTML(int id)
            => RedirectToAction("Index", "RenderCrfComponent", new { id });

        public IActionResult RenderReceipt(int id)
            => RedirectToAction("RenderReceipt", "RenderCrfComponent", new { id });

        public IActionResult RenderCSharpClass(int id)
            => RedirectToAction("RenderCSharpClass", "RenderCrfComponent", new { id });

        public async Task<IActionResult> Duplicate(int id)
        {
            try
            {
                var page = await _manager.GetByIdAsync(id);
                await _manager.DuplicateAsync(page);
                return RedirectToAction("Index", new CrfPageFilter { PartialName = page.Name });
            }
            catch (Exception ex)
            {
                // log error, optionally show UI error message
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var page = await _manager.GetByIdAsync(id.Value);
            return page == null ? NotFound() : View(page);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrfPage page)
        {
            if (!ModelState.IsValid) return View(page);
            _manager.Update(page); // Handles ModifiedDateTime
            await _manager.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var page = await _manager.GetByIdAsync(id.Value);
            if (page == null) return NotFound();
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            return View(page);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CrfPage page)
        {
            if (!ModelState.IsValid) return View(page);
            if (page.Id == 0) return NotFound();
            try
            {
                _manager.Update(page);
                await _manager.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_manager.Exists(page.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var page = await _manager.GetByIdAsync(id.Value);
            if (page == null) return NotFound();
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            return View(page);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _manager.GetByIdAsync(id);
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            page.IsDeleted = true;
            await _manager.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Undelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UndeleteConfirmed(int id)
        {
            var page = await _manager.GetByIdAsync(id);
            page.IsDeleted = false;
            await _manager.SaveAsync();
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ReturnLockedMessage(CrfPage page)
            => View(page);
    }

}
