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
    public class IssuesController : Controller
    {
        private readonly MiniMagContext _context;

        public IssuesController(MiniMagContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            var miniMagContext = _context.Issue.Include(i => i.Product);
            return View(await miniMagContext.OrderDescending().ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            var model = new Issue()
            {
                Date = DateTime.Today
            };

            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name");
            return View(model);
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductID,Quantity,Date,Notes")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Product
                    .FirstOrDefaultAsync(p => p.ID == issue.ProductID);

                if (product == null)
                {
                    return NotFound();
                }

                if (product.Quantity - issue.Quantity < 0)
                {
                    ModelState.AddModelError("", "Cannot issue more than the number of products in inventory. (" + product.Quantity.ToString() + ")");
                    ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", issue.ProductID);
                    return View(issue);
                }

                product.Quantity -= issue.Quantity;

                _context.Add(issue);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", issue.ProductID);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", issue.ProductID);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,Quantity,Date,Notes")] Issue issue)
        {
            if (id != issue.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.ID))
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
            ViewData["ProductID"] = new SelectList(_context.Product.OrderBy(p => p.Name), "ID", "Name", issue.ProductID);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issue.FindAsync(id);
            if (issue != null)
            {
                _context.Issue.Remove(issue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issue.Any(e => e.ID == id);
        }
    }
}
