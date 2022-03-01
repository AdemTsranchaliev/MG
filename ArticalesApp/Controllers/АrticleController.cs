using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticalesApp.Data;
using ArticalesApp.Models;

namespace ArticalesApp.Controllers
{
    public class АrticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public АrticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Аrticle
        public async Task<IActionResult> Index()
        {
            return View(await _context.Аrticle.ToListAsync());
        }

        // GET: Аrticle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var аrticle = await _context.Аrticle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (аrticle == null)
            {
                return NotFound();
            }

            return View(аrticle);
        }

        // GET: Аrticle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Аrticle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Contents,Photo")] Аrticle аrticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(аrticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(аrticle);
        }

        // GET: Аrticle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var аrticle = await _context.Аrticle.FindAsync(id);
            if (аrticle == null)
            {
                return NotFound();
            }
            return View(аrticle);
        }

        // POST: Аrticle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Contents,Photo")] Аrticle аrticle)
        {
            if (id != аrticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(аrticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!АrticleExists(аrticle.Id))
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
            return View(аrticle);
        }

        // GET: Аrticle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var аrticle = await _context.Аrticle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (аrticle == null)
            {
                return NotFound();
            }

            return View(аrticle);
        }

        // POST: Аrticle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var аrticle = await _context.Аrticle.FindAsync(id);
            _context.Аrticle.Remove(аrticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool АrticleExists(int id)
        {
            return _context.Аrticle.Any(e => e.Id == id);
        }
    }
}
