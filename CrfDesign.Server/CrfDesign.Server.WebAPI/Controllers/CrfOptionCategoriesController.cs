using BuisnessLogic.Filters;
using BuisnessLogic.Models;
using CrfDesign.Server.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CrfOptionCategoriesController : Controller
    {
        private readonly CrfOptionCategoryManager _manager;

        public CrfOptionCategoriesController(CrfOptionCategoryManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index(CrfOptionCategoriesFilter filter)
        {
            var result = await _manager.GetAllAsync(filter);
            if (result == null)
                return View(new List<CrfOptionCategory>());

            ViewBag.filter = filter;
            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var result = await _manager.GetDetailsAsync(id);
            return result == null ? NotFound() : View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrfOptionCategory crfOptionCategory)
        {
            if (!ModelState.IsValid)
                return View(crfOptionCategory);

            var success = await _manager.CreateAsync(crfOptionCategory);
            return success ? RedirectToAction(nameof(Index)) : View(crfOptionCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _manager.GetEditAsync(id);
            return result == null ? NotFound() : View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CrfOptionCategory crfOptionCategory)
        {
            if (id != crfOptionCategory.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(crfOptionCategory);

            var success = await _manager.EditAsync(crfOptionCategory);
            return success ? RedirectToAction(nameof(Index)) : View(crfOptionCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _manager.GetDeleteAsync(id);
            return result == null ? NotFound() : View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _manager.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Duplicate(int? id, CrfOptionCategoriesFilter filter)
        {
            return RedirectToAction(nameof(Index), filter);
        }
    }
}
