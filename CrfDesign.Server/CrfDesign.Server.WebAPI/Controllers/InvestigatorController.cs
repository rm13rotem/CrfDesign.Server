using CrfDesign.Server.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InvestigatorsController : Controller
    {
        private readonly UserManager<Investigator> _userManager;

        public InvestigatorsController(UserManager<Investigator> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Investigator model, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(string id, Investigator model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Update properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.QuickLookId = model.QuickLookId;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;

            // ✅ Persist changes
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            // If it failed, show errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
