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
using Microsoft.AspNetCore.Authorization;
using BuisnessLogic.Repositories;

namespace CrfDesign.Server.WebAPI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class QuestionTypesController : Controller
    {
        private readonly IInMemoryCrfDataStore _context;

        public QuestionTypesController(IInMemoryCrfDataStore dataStore)
        {
            _context = dataStore;
        }

        // GET: QuestionTypes
        public IActionResult Index()
        {
            return View(_context.QuestionTypes);
        }

        // GET: QuestionTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = _context.QuestionTypes
                .FirstOrDefault(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // GET: QuestionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _context.AddAsync(questionType);
                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = _context.QuestionTypes.FirstOrDefault(x => x.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }
            return View(questionType);
        }

        // POST: QuestionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,ModifiedDateTime")] QuestionType questionType)
        {
            if (id != questionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateAsync(questionType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionTypeExists(questionType.Id))
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
            return View(questionType);
        }

        // GET: QuestionTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionType = _context.QuestionTypes
                .FirstOrDefault(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // POST: QuestionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteAsync<QuestionType>(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnDelete(int id)
        {
            await _context.UndeleteAsync<QuestionType>(id);
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionTypeExists(int id)
        {
            return _context.QuestionTypes.Any(e => e.Id == id);
        }
    }
}
