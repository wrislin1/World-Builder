using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorldBuilder.Data;
using WorldBuilder.Models;

namespace WorldBuilder.Controllers
{
    public class LoresController : Controller
    {
        private readonly WorldContext _context;

        public LoresController(WorldContext context)
        {
            _context = context;
        }

        // GET: Lores
        public async Task<IActionResult> Index()
        {
            var worldContext = _context.Lores.Include(l => l.World);
            return View(await worldContext.ToListAsync());
        }

        // GET: Lores/Details/5
        public async Task<IActionResult> Lore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = await _context.Lores
                .Include(l => l.World)
                .FirstOrDefaultAsync(m => m.LoreID == id);
            if (lore == null)
            {
                return NotFound();
            }

            return View(lore);
        }

        // GET: Lores/Create
        public IActionResult Create()
        {
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name");
            return View();
        }

        // POST: Lores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoreID,WorldID,Title,Details")] Lore lore)
        {
            if (ModelState.IsValid && lore.Title!=null)
            {
                _context.Add(lore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", lore.WorldID);
            return View(lore);
        }

        // GET: Lores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = await _context.Lores.FindAsync(id);
            if (lore == null)
            {
                return NotFound();
            }
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", lore.WorldID);
            return View(lore);
        }

        // POST: Lores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoreID,WorldID,Title,Details")] Lore lore)
        {
            if (id != lore.LoreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoreExists(lore.LoreID))
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
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", lore.WorldID);
            return View(lore);
        }

        // GET: Lores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lore = await _context.Lores
                .Include(l => l.World)
                .FirstOrDefaultAsync(m => m.LoreID == id);
            if (lore == null)
            {
                return NotFound();
            }

            return View(lore);
        }

        // POST: Lores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lore = await _context.Lores.FindAsync(id);
            _context.Lores.Remove(lore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoreExists(int id)
        {
            return _context.Lores.Any(e => e.LoreID == id);
        }
    }
}
