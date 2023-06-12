using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TossUpsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TossUpsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TossUps
        public async Task<IActionResult> Index()
        {
              return _context.TossUp != null ? 
                          View(await _context.TossUp.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TossUp'  is null.");
        }

        // GET: TossUps/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: TossUps/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string searchPhrase)
        {
            return _context.TossUp != null ?
                           View("Index",await _context.TossUp.Where(q => q.Question.Contains(searchPhrase)).ToListAsync()) :
                           Problem("Entity set 'ApplicationDbContext.TossUp'  is null.");
        }

        // GET: TossUps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TossUp == null)
            {
                return NotFound();
            }

            var tossUp = await _context.TossUp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tossUp == null)
            {
                return NotFound();
            }

            return View(tossUp);
        }

        // GET: TossUps/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TossUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] TossUp tossUp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tossUp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tossUp);
        }

        // GET: TossUps/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TossUp == null)
            {
                return NotFound();
            }

            var tossUp = await _context.TossUp.FindAsync(id);
            if (tossUp == null)
            {
                return NotFound();
            }
            return View(tossUp);
        }

        // POST: TossUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] TossUp tossUp)
        {
            if (id != tossUp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tossUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TossUpExists(tossUp.Id))
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
            return View(tossUp);
        }

        // GET: TossUps/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TossUp == null)
            {
                return NotFound();
            }

            var tossUp = await _context.TossUp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tossUp == null)
            {
                return NotFound();
            }

            return View(tossUp);
        }

        // POST: TossUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TossUp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TossUp'  is null.");
            }
            var tossUp = await _context.TossUp.FindAsync(id);
            if (tossUp != null)
            {
                _context.TossUp.Remove(tossUp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TossUpExists(int id)
        {
          return (_context.TossUp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
