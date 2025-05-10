using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Controllers
{
    public class GorevController : Controller
    {
        private readonly AppDbContext _context;
        public GorevController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gorev
        public async Task<IActionResult> Index()
        {
            var liste = await _context.Gorevler
                                      .Include(g => g.Proje)
                                      .ToListAsync();
            return View(liste);
        }

        // GET: Gorev/Create
        public IActionResult Create()
        {
            PopulateProjeDropDown();
            return View();
        }

        // POST: Gorev/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gorev gorev)
        {
            // RowVersion formda yok; model state'ten kaldır, hata olmasın
            ModelState.Remove(nameof(gorev.RowVersion));

            if (!ModelState.IsValid)
            {
                PopulateProjeDropDown(gorev.ProjeId);
                return View(gorev);
            }

            _context.Add(gorev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Gorev/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var gorev = await _context.Gorevler.FindAsync(id);
            if (gorev == null) return NotFound();

            PopulateProjeDropDown(gorev.ProjeId);
            return View(gorev);
        }

        // POST: Gorev/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gorev gorev)
        {
            if (id != gorev.GorevId) return NotFound();

            ModelState.Remove(nameof(gorev.RowVersion));

            if (!ModelState.IsValid)
            {
                PopulateProjeDropDown(gorev.ProjeId);
                return View(gorev);
            }

            try
            {
                _context.Update(gorev);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Gorevler.AnyAsync(e => e.GorevId == gorev.GorevId))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Gorev/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var gorev = await _context.Gorevler
                .Include(g => g.Proje)
                .FirstOrDefaultAsync(m => m.GorevId == id);
            if (gorev == null) return NotFound();

            return View(gorev);
        }

        // POST: Gorev/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, byte[] rowVersion)
        {
            var gorev = await _context.Gorevler.FindAsync(id);
            if (gorev == null) return RedirectToAction(nameof(Index));

            _context.Entry(gorev).Property("RowVersion").OriginalValue = rowVersion;

            try
            {
                _context.Gorevler.Remove(gorev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Bu görev başka bir kullanıcı tarafından değiştirilmiş veya silinmiş.");
                return View(gorev);
            }
        }

        // Yardımcı: Proje dropdown’u doldurur
        private void PopulateProjeDropDown(object selectedProje = null)
        {
            var projeler = _context.Projeler
                                   .OrderBy(p => p.Baslik)
                                   .Select(p => new { p.ProjeId, p.Baslik })
                                   .ToList();
            ViewData["ProjeId"] = new SelectList(projeler, "ProjeId", "Baslik", selectedProje);
        }
    }
}
