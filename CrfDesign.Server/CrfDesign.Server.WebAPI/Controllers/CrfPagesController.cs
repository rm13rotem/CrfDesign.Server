using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.Filters;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [Authorize]
    public class CrfPagesController : Controller
    {
        private readonly CrfPageManager _manager;
        private readonly ILogger<CrfPagesController> _logger;

        public CrfPagesController(UserManager<Investigator> userManager,
            IInMemoryCrfDataStore dataStore,
            IServiceScopeFactory scopeFactory,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CrfPagesController> logger)
        {
            _manager = new CrfPageManager(dataStore, userManager, scopeFactory, httpContextAccessor);
            _logger = logger;
        }

        public async Task<IActionResult> Index(CrfPageFilter filter)
        {
            var pages = await _manager.GetFilteredPagesAsync(filter);
            ViewBag.filter = filter ?? new CrfPageFilter()
            {
                PartialName = string.Empty
            };
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
                var errorMsg = string.Format("Duplication of page {0} failed", id);
                _logger.LogError(ex, errorMsg);
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var page = _manager.GetById(id.Value);
            return page == null ? NotFound() : View(page);
        }

        public IActionResult Create() => View();


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CrfPage page)
        {
            if (!ModelState.IsValid)
                return View(page);
            _manager.UpdateAsync(page); // Handles ModifiedDateTime
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var page = _manager.GetById(id);
            if (page == null) return NotFound();
            if (_manager.IsPageLockedForChanges(id))
                return RedirectToAction("ReturnLockedMessage", page);
            return View(page);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CrfPage page)
        {
            if (page?.Id == 0 || page?.Id == null) return NotFound();
            if (_manager.IsPageLockedForChanges(page.Id))
                return RedirectToAction("ReturnLockedMessage", page);

            if (ModelState.IsValid)
            {
                bool isSuccess;
                try
                {
                    isSuccess = await _manager.UpdateAsync(page);
                    if (isSuccess)
                        return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "DbUpdate Concurrency Exception in CrfPageController");
                    if (!_manager.Exists(page.Id))
                        return NotFound();
                    isSuccess = false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unknown Exception in CrfPageController");
                    isSuccess = false;
                }
            }
            return View(page);
        }

        public IActionResult Lock(int? id)
        {
            if (id == null) return NotFound();
            var page = _manager.GetById((int)id);
            _manager.LockAsync(page);
            var filter = new CrfPageFilter()
            {
                PartialName = page.Name
            };
            return RedirectToAction(nameof(Index), filter);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var page = _manager.GetById(id.Value);
            if (page == null) return NotFound();
            if (_manager.IsPageLockedForChanges(id.Value))
                return RedirectToAction("ReturnLockedMessage", page);
            return View(page);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = _manager.GetById(id);
            if (_manager.IsPageLockedForChanges(id))
                return RedirectToAction("ReturnLockedMessage", page);
            page.IsDeleted = true;
            bool isSuccess = await _manager.UpdateAsync(page);
            if (isSuccess)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Delete), id);
        }

        [HttpPost, ActionName("Undelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UndeleteConfirmed(int id)
        {
            var page = _manager.GetById(id);
            if (_manager.IsPageLockedForChanges(id))
                return RedirectToAction("ReturnLockedMessage", page);
            page.IsDeleted = false;

            await _manager.UpdateAsync(page);
            var filter = new CrfPageFilter() { PartialName = page.Name };
            return RedirectToAction(nameof(Index), filter);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ReloadCache()
        {
            _manager.DataStore_Refresh();

            return Ok("Cache reloaded successfully.");
        }

        public IActionResult ReturnLockedMessage(CrfPage page)
            => View(page);
    }

}
