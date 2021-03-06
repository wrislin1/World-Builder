﻿using System;
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
    public class LocationsController : Controller
    {
        private readonly WorldContext _context;

        public LocationsController(WorldContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var worldContext = _context.Locations.Include(l => l.World);
            return View(await worldContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Location(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.World)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (location == null)
            {
                return NotFound();
            }

            if (_context.Characters.Where(m => m.LocationID == id).Any())
                ViewBag.data = await _context.Characters.Where(m => m.LocationID == id).ToListAsync();
            else
                ViewBag.data = null;

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create(int? id)
        {
            if(id==null)
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name");
            else
                ViewData["WorldID"] = new SelectList(_context.Worlds.Where(w=>w.WorldID==id), "WorldID", "Name");

            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,WorldID,Name,Summary")] Location location)
        {
            if (ModelState.IsValid&&location.Name!=null)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", location.WorldID);
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", location.WorldID);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,WorldID,Name,Summary")] Location location)
        {
            if (id != location.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var change = _context.Locations.AsNoTracking().FirstOrDefault(m => m.LocationID == id);
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                    if (change.WorldID!=location.WorldID)
                    {
                        var worldContext = await _context.Characters.Include(c => c.World).Where(m => m.LocationID == location.LocationID).ToListAsync();
                        foreach(var character in worldContext)
                        {
                            character.World = location.World;
                            character.WorldID = location.WorldID;
                            _context.Update(character);
                            await _context.SaveChangesAsync();
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationID))
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
            ViewData["WorldID"] = new SelectList(_context.Worlds, "WorldID", "Name", location.WorldID);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.World)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
        }
    }
}
