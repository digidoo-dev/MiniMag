using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMag.Data;
using MiniMag.Models;

namespace MiniMag.Controllers
{
    public class IntakesController : Controller
    {
        private readonly MiniMagContext _context;

        public IntakesController(MiniMagContext context)
        {
            _context = context;
        }

        // GET: Intakes
        public async Task<IActionResult> Index()
        {
            var miniMagContext = _context.Intake.Include(i => i.Product).Include(i => i.Supplier);
            return View(await miniMagContext.OrderDescending().ToListAsync());
        }

        // GET: Intakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intake
                .Include(i => i.Product)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // GET: Intakes/Create
        public IActionResult Create()
        {
            var model = new Intake()
            {
                Date = DateTime.Today
            };
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Supplier.OrderBy(s => s.Name), "ID", "Name");
            return View(model);
        }

        // POST: Intakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductID,SupplierID,Quantity,Date,Notes")] Intake intake)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Product
                    .FirstOrDefaultAsync(p => p.ID == intake.ProductID);
                
                if (product == null)
                {
                    return NotFound();
                }

                product.Quantity += intake.Quantity;

                _context.Add(intake);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", intake.ProductID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier.OrderBy(s => s.Name), "ID", "Name", intake.SupplierID);
            return View(intake);
        }

        // GET: Intakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intake.FindAsync(id);
            if (intake == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", intake.ProductID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier.OrderBy(s => s.Name), "ID", "Name", intake.SupplierID);
            return View(intake);
        }

        // POST: Intakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,SupplierID,Quantity,Date,Notes")] Intake intake)
        {
            if (id != intake.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakeExists(intake.ID))
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
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", intake.ProductID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier.OrderBy(s => s.Name), "ID", "Name", intake.SupplierID);
            return View(intake);
        }

        // GET: Intakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intake
                .Include(i => i.Product)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // POST: Intakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intake = await _context.Intake.FindAsync(id);
            if (intake != null)
            {
                _context.Intake.Remove(intake);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntakeExists(int id)
        {
            return _context.Intake.Any(e => e.ID == id);
        }
    }
}
