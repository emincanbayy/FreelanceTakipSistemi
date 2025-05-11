using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceTakipSistemi.Controllers
{
    /// <summary>
    /// Proje yönetimi için CRUD işlemlerini sağlar.
    /// </summary>
    [Authorize]  // Proje işlemleri için kullanıcı girişi zorunlu
    public class ProjeController : Controller
    {
        private readonly AppDbContext _context;

        public ProjeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Giriş yapmış kullanıcının (veya adminin tüm) projelerini listeler.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var sorgu = _context.Projeler
                                .Include(p => p.Kullanici)
                                .AsNoTracking();

            if (!User.IsInRole("Admin"))
                sorgu = sorgu.Where(p => p.KullaniciId == userId);

            var model = await sorgu
                .OrderByDescending(p => p.BaslangicTarihi)
                .ToListAsync();

            return View(model);
        }

        /// <summary>
        /// Yeni proje oluşturma formunu getirir.
        /// </summary>
        public IActionResult Create()
        {
            return View(new Proje());
        }

        /// <summary>
        /// Yeni projeyi veritabanına ekler.
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proje model)
        {
            // Oturum açmış kullanıcının ID'sini ata
            model.KullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Navigation property validasyon hatalarını temizle
            ModelState.Remove(nameof(model.Kullanici));
            ModelState.Remove(nameof(model.Gorevler));

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _context.Projeler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                // InnerException mesajı, eksik veya hatalı sütun bilgisini içerir
                var error = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {error}");
                return View(model);
            }
        }

        /// <summary>
        /// Var olan bir projeyi düzenleme formunu getirir.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var proje = await _context.Projeler
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(p => p.ProjeId == id);
            if (proje == null)
                return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!User.IsInRole("Admin") && proje.KullaniciId != userId)
                return Forbid();

            return View(proje);
        }

        /// <summary>
        /// Düzenlenen projeyi günceller.
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proje model)
        {
            if (id != model.ProjeId)
                return BadRequest();

            // Navigation property validasyon hatalarını temizle
            ModelState.Remove(nameof(model.Kullanici));
            ModelState.Remove(nameof(model.Gorevler));

            if (!ModelState.IsValid)
                return View(model);

            var mevcut = await _context.Projeler
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.ProjeId == id);
            if (mevcut == null)
                return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!User.IsInRole("Admin") && mevcut.KullaniciId != userId)
                return Forbid();

            // Sahibiyi koru
            model.KullaniciId = mevcut.KullaniciId;

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
                return View(model);
            }
        }

        /// <summary>
        /// Proje silme onay formunu getirir.
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var proje = await _context.Projeler
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(p => p.ProjeId == id);
            if (proje == null)
                return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!User.IsInRole("Admin") && proje.KullaniciId != userId)
                return Forbid();

            return View(proje);
        }

        /// <summary>
        /// Onaylanan projeyi siler.
        /// </summary>
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proje = await _context.Projeler.FindAsync(id);
            if (proje == null)
                return RedirectToAction(nameof(Index));

            try
            {
                _context.Projeler.Remove(proje);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {error}");
                return View(proje);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
