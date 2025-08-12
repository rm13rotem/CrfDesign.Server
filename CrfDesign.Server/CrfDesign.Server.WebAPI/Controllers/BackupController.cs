using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuisnessLogic.DataContext;
using BuisnessLogic.Models;
using CrfDesign.Server.WebAPI.Models.Backup;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using CrfDesign.Server.WebAPI.Data;
using BuisnessLogic.Repositories;

namespace CrfDesign.Server.WebAPI.Controllers
{
    [Authorize(Roles="Admin")]
    public class BackupController : Controller
    {
        private readonly IInMemoryCrfDataStore _context;
        private readonly ApplicationDbContext _userContext;

        public BackupController(IInMemoryCrfDataStore dataStore,
            ApplicationDbContext userContext)
        {
            _context = dataStore;
            _userContext = userContext;
        }

        // GET: Backup Users
        public async Task<IActionResult> SaveUsers()
        {
            LoginUserViewModel db = new LoginUserViewModel(_userContext);
            object dbJson = JsonConvert.SerializeObject(db);
            return View(dbJson);
        }

        // GET: Backup
        public async Task<IActionResult> Save()
        {
            DataBaseViewModel db = new DataBaseViewModel(_context);
            object dbJson = JsonConvert.SerializeObject(db);
            return View(dbJson);
        }

        // GET: Backup/Load/
        public IActionResult Load()
        {
            LoadOptions model = new();
            model.ModifiedDateTime = DateTime.UtcNow;
            return View(model);
        }

        // POST: Backup/Load
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Load(LoadOptions model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await model.SaveToDb(_context);
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                    // in the future - Log. (Serilog)
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return View(model);
        }


        // GET: Backup/LoadUsers/
        public IActionResult LoadUsers()
        {
            LoadUserOptions model = new();
            model.ModifiedDateTime = DateTime.UtcNow;
            return View(model);
        }

        // POST: Backup/LoadUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadUsers(LoadUserOptions model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await model.SaveToDb(_userContext);
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                    // in the future - Log. (Serilog)
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return View(model);
        }


    }
}
