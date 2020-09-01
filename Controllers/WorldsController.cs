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
    public class WorldsController : Controller
    {
        private readonly WorldContext _context;

        public WorldsController(WorldContext context)
        {
            _context = context;
        }

        // GET: Worlds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Worlds.ToListAsync());
        }

        // GET: Worlds/Details/5
        public async Task<IActionResult> World(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var world = await _context.Worlds
                .FirstOrDefaultAsync(m => m.WorldID == id);
            if (world == null)
            {
                return NotFound();
            }
            if (_context.Characters.Where(m => m.WorldID == id).Any())
                ViewBag.data = await _context.Characters.Where(m => m.WorldID == id).ToListAsync();
            else
                ViewBag.data = null;

            if (_context.Locations.Where(m => m.WorldID == id).Any())
                ViewBag.loc = await _context.Locations.Where(m => m.WorldID == id).ToListAsync();
            else
                ViewBag.loc = null;

            if (_context.Lores.Where(m => m.WorldID == id).Any())
                ViewBag.lore = await _context.Lores.Where(m => m.WorldID == id).ToListAsync();
            else
                ViewBag.lore = null;

            return View(world);
        }

        // GET: Worlds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Worlds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Summary,WorldID")] World world)
        {
            if (ModelState.IsValid&&world.Name!=null)
            {
                _context.Add(world);
                await _context.SaveChangesAsync();
                return RedirectToAction("World","Worlds",new {id=world.WorldID });

            }
            return View(world);
        }

        // GET: Worlds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var world = await _context.Worlds.FindAsync(id);
            if (world == null)
            {
                return NotFound();
            }
            return View(world);
        }

        // POST: Worlds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Summary,WorldID")] World world)
        {
            if (id != world.WorldID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(world);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldExists(world.WorldID))
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
            return View(world);
        }

        // GET: Worlds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var world = await _context.Worlds
                .FirstOrDefaultAsync(m => m.WorldID == id);
            if (world == null)
            {
                return NotFound();
            }

            return View(world);
        }

        // POST: Worlds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var world = await _context.Worlds.FindAsync(id);
            _context.Worlds.Remove(world);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorldExists(int id)
        {
            return _context.Worlds.Any(e => e.WorldID == id);
        }
    }
}
