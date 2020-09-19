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
    public class RelationshipsController : Controller
    {
        private readonly WorldContext _context;

        public RelationshipsController(WorldContext context)
        {
            _context = context;
        }

        // GET: Relationships
        public async Task<IActionResult> Index()
        {
            var worldContext = _context.Relationships.Include(r => r.RelationshipType);
            return View(await worldContext.ToListAsync());
        }

        // GET: Relationships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Relationship = await _context.Relationships
                .Include(r => r.RelationshipType)
                .FirstOrDefaultAsync(m => m.RelationshipID == id);
            if (Relationship == null)
            {
                return NotFound();
            }

            return View(Relationship);
        }

        // GET: Relationships/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.char1 = new SelectList(_context.Characters.Where(c => c.CharacterID == id), "CharacterID","Name");

            ViewBag.chars = new SelectList(_context.Characters.Where(c => c.CharacterID != id), "CharacterID", "Name");

            ViewData["RelationshipTypeID"] = new SelectList(_context.RelationshipTypes, "RelationshipTypeID", "Description");
            return View();
        }

        // POST: Relationships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationshipID,Character1ID,Character2ID,RelationshipTypeID,Details")] Relationship Relationship)
        {
            bool related = false;
            var context = _context.Relationships.Where(c => c.Character1ID == Relationship.Character1ID).ToList();
            foreach(var c in context)
            {
                if (c.Character2ID == Relationship.Character2ID)
                {
                    related = true;
                    break;
                }
            }
            
            if (ModelState.IsValid&&!related)
            {
               await UpdateRelationsAsync(Relationship);
                _context.Add(Relationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.related=true;
            ViewData["RelationshipTypeID"] = new SelectList(_context.RelationshipTypes, "RelationshipTypeID", "Description", Relationship.RelationshipTypeID);
            ViewBag.char1 = new SelectList(_context.Characters.Where(c => c.CharacterID == Relationship.Character1ID), "CharacterID", "Name");
            ViewBag.chars = new SelectList(_context.Characters.Where(c => c.CharacterID != Relationship.Character1ID), "CharacterID", "Name");
            return View();
        }

        // GET: Relationships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Relationship = await _context.Relationships.FindAsync(id);
            if (Relationship == null)
            {
                return NotFound();
            }
            ViewData["RelationshipTypeID"] = new SelectList(_context.RelationshipTypes, "RelationshipTypeID", "Description", Relationship.RelationshipTypeID);
            return View(Relationship);
        }

        // POST: Relationships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelationshipID,Character1ID,Character2ID,RelationshipTypeID,Details")] Relationship Relationship)
        {
            if (id != Relationship.RelationshipID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Relationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationshipExists(Relationship.RelationshipID))
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
            ViewData["RelationshipTypeID"] = new SelectList(_context.RelationshipTypes, "RelationshipTypeID", "Description", Relationship.RelationshipTypeID);
            return View(Relationship);
        }

        // GET: Relationships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Relationship = await _context.Relationships
                .Include(r => r.RelationshipType)
                .FirstOrDefaultAsync(m => m.RelationshipID == id);
            if (Relationship == null)
            {
                return NotFound();
            }

            return View(Relationship);
        }

        // POST: Relationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Relationship = await _context.Relationships.FindAsync(id);
            var char2id = Relationship.Character2ID;
            var Relationship2 = await _context.Relationships.FirstOrDefaultAsync(r => r.Character1ID == Relationship.Character2ID && r.Character2ID == Relationship.Character1ID);
            _context.Relationships.Remove(Relationship);
            _context.Relationships.Remove(Relationship2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationshipExists(int id)
        {
            return _context.Relationships.Any(e => e.RelationshipID == id);
        }

        private async Task UpdateRelationsAsync(Relationship r)
        {
            //var character = _context.Characters.AsNoTracking().FirstOrDefault(c => c.CharacterID == r.Character2ID);
            Relationship relationship = new Relationship();
            switch (r.RelationshipTypeID)
            {
                case 1:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 9;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 2:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 2;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 3:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 3;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 4:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 8;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 5:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 5;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 6:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 6;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 7:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 7;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 8:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 4;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
                case 9:
                    relationship.Character1ID = r.Character2ID;
                    relationship.Character2ID = r.Character1ID;
                    relationship.RelationshipTypeID = 1;
                    _context.Add(relationship);
                    await _context.SaveChangesAsync();
                    break;
            }
        }
    }
}
