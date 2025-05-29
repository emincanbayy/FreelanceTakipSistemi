using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FreelanceTakipSistemi.Controllers
{
    [Authorize] // Giriş yapmış herkes erişebilir
    public class ProjeController : Controller
    {
        private readonly AppDbContext _context;

        public ProjeController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm projeleri listeler (admin ya da normal kullanıcı fark etmez)
        public async Task<IActionResult> Index()
        {
            var projeler = await _context.Projeler
                .Include(p => p.Kullanici)
                .Include(p => p.Sirket)
                .AsNoTracking()
                .OrderByDescending(p => p.BaslangicTarihi)
                .ToListAsync();

            return View(projeler);
        }

        // Proje detay sayfası (herkes erişebilir)
        public async Task<IActionResult> Details(int id)
        {
            var proje = await _context.Projeler
                .Include(p => p.Kullanici)
                .Include(p => p.Sirket)
                .FirstOrDefaultAsync(p => p.ProjeId == id);

            if (proje == null)
                return NotFound();

            return View(proje);
        }

        // SADECE ADMIN → Yeni proje oluşturma formu
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            ViewBag.Sirketler = new SelectList(
                _context.Sirketler.OrderBy(s => s.Ad).ToList(),
                "Id", "Ad");
            return View(new Proje());
        }

        // SADECE ADMIN → Yeni proje ekle
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(Proje model)
        {
            model.KullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ModelState.Remove(nameof(model.Kullanici));
            ModelState.Remove(nameof(model.Gorevler));

            if (!ModelState.IsValid)
            {
                ViewBag.Sirketler = new SelectList(
                    _context.Sirketler.OrderBy(s => s.Ad).ToList(),
                    "Id", "Ad", model.SirketId);
                return View(model);
            }

            try
            {
                _context.Projeler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {error}");
                ViewBag.Sirketler = new SelectList(
                    _context.Sirketler.OrderBy(s => s.Ad).ToList(),
                    "Id", "Ad", model.SirketId);
                return View(model);
            }
        }

        // SADECE ADMIN → Proje düzenleme formu
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int id)
        {
            var proje = await _context.Projeler
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjeId == id);
            if (proje == null) return NotFound();

            ViewBag.Sirketler = new SelectList(
                _context.Sirketler.OrderBy(s => s.Ad).ToList(),
                "Id", "Ad", proje.SirketId);
            return View(proje);
        }

        // SADECE ADMIN → Proje güncelleme işlemi
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int id, Proje model)
        {
            if (id != model.ProjeId) return BadRequest();

            ModelState.Remove(nameof(model.Kullanici));
            ModelState.Remove(nameof(model.Gorevler));

            if (!ModelState.IsValid)
            {
                ViewBag.Sirketler = new SelectList(
                    _context.Sirketler.OrderBy(s => s.Ad).ToList(),
                    "Id", "Ad", model.SirketId);
                return View(model);
            }

            try
            {
                _context.Projeler.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {error}");
                ViewBag.Sirketler = new SelectList(
                    _context.Sirketler.OrderBy(s => s.Ad).ToList(),
                    "Id", "Ad", model.SirketId);
                return View(model);
            }
        }

        // SADECE ADMIN → Proje silme onayı
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var proje = await _context.Projeler
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjeId == id);
            if (proje == null) return NotFound();

            return View(proje);
        }

        // SADECE ADMIN → Proje silme işlemi
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proje = await _context.Projeler.FindAsync(id);
            if (proje == null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Projeler.Remove(proje);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {ex.InnerException?.Message ?? ex.Message}");
                return View(proje);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
