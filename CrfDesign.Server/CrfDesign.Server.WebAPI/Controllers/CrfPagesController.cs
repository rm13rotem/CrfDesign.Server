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
using Microsoft.AspNetCore.Authorization;
using BuisnessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [Authorize]
    public class CrfPagesController : Controller
    {
        private readonly CrfPageManager _manager;

        public CrfPagesController(UserManager<Investigator> userManager, 
            IInMemoryCrfDataStore dataStore, IServiceScopeFactory scopeFactory)
        {
            _manager = new CrfPageManager(dataStore, userManager, scopeFactory);
        }

        public async Task<IActionResult> Index(CrfPageFilter filter)
        {
            var pages = await _manager.GetFilteredPagesAsync(filter);
            ViewBag.filter = filter;
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
                var page = _manager.GetById(id);
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
            var page = _manager.GetById(id.Value);
            return page == null ? NotFound() : View(page);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrfPage page)
        {
            if (!ModelState.IsValid) return View(page);
            _manager.Update(page); // Handles ModifiedDateTime
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var page = _manager.GetById(id.Value);
            if (page == null) return NotFound();
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            return View(page);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CrfPage page)
        {
            if (page.Id == 0) return NotFound();
            if (ModelState.IsValid)
            {
                bool isSuccess;
                try
                {
                    isSuccess = _manager.Update(page);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_manager.Exists(page.Id)) return NotFound();
                    isSuccess = false;
                }
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var page = _manager.GetById(id.Value);
            if (page == null) return NotFound();
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            return View(page);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var page = _manager.GetById(id);
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            page.IsDeleted = true;
            bool isSuccess = _manager.Update(page);
            if (isSuccess)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Delete), id);
        }

        [HttpPost, ActionName("Undelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UndeleteConfirmed(int id)
        {
            var page = _manager.GetById(id);
            if (page.IsLockedForChanges) return RedirectToAction("ReturnLockedMessage", page);
            page.IsDeleted = false;
            _manager.Update(page);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles="Admin")]
        public IActionResult ReloadCache()
        {
            _manager.StoreRefresh();

            return Ok("Cache reloaded successfully.");
        }
        public IActionResult ReturnLockedMessage(CrfPage page)
            => View(page);
    }

}
