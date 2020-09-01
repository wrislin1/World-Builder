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
    public class CharactersController : Controller
    {
        private readonly WorldContext _context;

        public CharactersController(WorldContext context)
        {
            _context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            var worldContext = _context.Characters.Include(c => c.Location).Include(c => c.World);
            return View(await worldContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Character(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Location)
                .Include(c => c.World)
                .FirstOrDefaultAsync(m => m.CharacterID == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name");
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CharacterID,WorldID,LocationID,Summary")] Character character)
        {
            
            ViewBag.data = null;

            if (ModelState.IsValid&&character.Name!=null)
            {
                var location = await _context.Locations.Include(l => l.World).FirstOrDefaultAsync(m => m.LocationID == character.LocationID);
                if (character.WorldID != location.WorldID)
                {

                    ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", character.LocationID);
                    ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", character.WorldID);
                    ViewBag.location = location;
                    return View(character);
                }
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", character.LocationID);
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", character.WorldID);
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", character.LocationID);
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", character.WorldID);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,CharacterID,WorldID,LocationID,Summary")] Character character)
        {
            if (id != character.CharacterID)
            {
                return NotFound();
            }

            var location = await _context.Locations.Include(l => l.World).FirstOrDefaultAsync(m => m.LocationID == character.LocationID);
            ViewBag.data = null;
            if (character.WorldID != location.WorldID)
            {
                ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", character.LocationID);
                ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", character.WorldID);
                ViewBag.location = location;
                return View(character);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.CharacterID))
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
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", character.LocationID);
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", character.WorldID);
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Location)
                .Include(c => c.World)
                .FirstOrDefaultAsync(m => m.CharacterID == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.CharacterID == id);
        }
    }
}
