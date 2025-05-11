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
        public GorevController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var liste = await _context.Gorevler
                .Include(g => g.Proje)
                .AsNoTracking()
                .OrderByDescending(g => g.OlusturmaTarihi)
                .ToListAsync();
            return View(liste);
        }

        public IActionResult Create()
        {
            PopulateProjeDropDown();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gorev gorev)
        {
            ModelState.Remove(nameof(gorev.RowVersion));
            if (!ModelState.IsValid)
            {
                PopulateProjeDropDown(gorev.ProjeId);
                return View(gorev);
            }
            _context.Gorevler.Add(gorev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var gorev = await _context.Gorevler.FindAsync(id.Value);
            if (gorev == null) return NotFound();
            PopulateProjeDropDown(gorev.ProjeId);
            return View(gorev);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gorev gorev)
        {
            if (id != gorev.Id) return BadRequest();
            ModelState.Remove(nameof(gorev.RowVersion));
            if (!ModelState.IsValid)
            {
                PopulateProjeDropDown(gorev.ProjeId);
                return View(gorev);
            }
            try
            {
                _context.Entry(gorev).Property(g => g.RowVersion).OriginalValue = gorev.RowVersion;
                _context.Gorevler.Update(gorev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Gorevler.AnyAsync(e => e.Id == gorev.Id)) return NotFound();
                ModelState.AddModelError(string.Empty, "Eşzamanlılık hatası, lütfen tekrar deneyin.");
                PopulateProjeDropDown(gorev.ProjeId);
                return View(gorev);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var gorev = await _context.Gorevler
                .Include(g => g.Proje)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id.Value);
            if (gorev == null) return NotFound();
            return View(gorev);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gorev = await _context.Gorevler.FindAsync(id);
            if (gorev == null) return RedirectToAction(nameof(Index));
            try
            {
                _context.Gorevler.Remove(gorev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Silme sırasında eşzamanlılık hatası oluştu.");
                return View(gorev);
            }
        }

        private void PopulateProjeDropDown(object selected = null)
        {
            var projeler = _context.Projeler
                .OrderBy(p => p.Baslik)
                .Select(p => new { p.ProjeId, p.Baslik })
                .ToList();
            ViewData["ProjeId"] = new SelectList(projeler, "ProjeId", "Baslik", selected);
        }
    }
}
