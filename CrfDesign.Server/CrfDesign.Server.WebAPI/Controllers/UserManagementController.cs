using CrfDesign.Server.WebAPI.Models;
using CrfDesign.Server.WebAPI.Models.LoginModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<Investigator> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            UserManager<Investigator> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult LoadRoles()
        {
            IdentityDataInitializer.SeedRolesAsync(_roleManager).Wait();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            var model = new EditUserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = allRoles.Select(role => new RoleSelection
                {
                    RoleName = role.Name,
                    IsSelected = userRoles.Contains(role.Name)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName);

            var resultRemove = await _userManager.RemoveFromRolesAsync(user, userRoles);
            var resultAdd = await _userManager.AddToRolesAsync(user, selectedRoles);

            if (resultRemove.Succeeded && resultAdd.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update roles.");
            return View(model);
        }
    }

}
