using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectEvidence.Data;
using ProjectEvidence.Models;

namespace ProjectEvidence.Controllers
{
    public class CoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Courses
                .FirstOrDefaultAsync(m => m.CoursID == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoursID,Title,Credits")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cours);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Courses.FindAsync(id);
            if (cours == null)
            {
                return NotFound();
            }
            return View(cours);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoursID,Title,Credits")] Cours cours)
        {
            if (id != cours.CoursID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursExists(cours.CoursID))
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
            return View(cours);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Courses
                .FirstOrDefaultAsync(m => m.CoursID == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cours = await _context.Courses.FindAsync(id);
            if (cours != null)
            {
                _context.Courses.Remove(cours);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursExists(int id)
        {
            return _context.Courses.Any(e => e.CoursID == id);
        }
    }
}
