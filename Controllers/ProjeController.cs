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
    [Authorize]  // Tüm Proje işlemleri için giriş yapılmış olmalı
    public class ProjeController : Controller
    {
        private readonly AppDbContext _context;

        public ProjeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Proje
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var sorgu = _context.Projeler.Include(p => p.Kullanici).AsQueryable();

            // Admin değilse sadece kendi projelerini görsün
            if (!User.IsInRole("Admin"))
                sorgu = sorgu.Where(p => p.KullaniciId == userId);

            var model = await sorgu.ToListAsync();
            return View(model);
        }

        // GET: /Proje/Create
        public IActionResult Create()
        {
            return View(new Proje());
        }

        // POST: /Proje/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proje model)
        {
            // Oturum açmış kullanıcının ID'sini atıyoruz
            model.KullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Navigation property'lerine ait validation hatalarını kaldır:
            ModelState.Remove(nameof(model.Kullanici));
            ModelState.Remove(nameof(model.Gorevler));

            if (!ModelState.IsValid)
            {
                // Hata varsa formu tekrar göster
                return View(model);
            }

            _context.Projeler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Proje/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var proje = await _context.Projeler.FindAsync(id);
            if (proje == null) return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!User.IsInRole("Admin") && proje.KullaniciId != userId)
                return Forbid();

            return View(proje);
        }

        // POST: /Proje/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proje model)
        {
            if (id != model.ProjeId)
                return BadRequest();

            // Navigation hatalarını temizle
            ModelState.Remove(nameof(model.Kullanici));
            ModelState.Remove(nameof(model.Gorevler));

            if (!ModelState.IsValid)
                return View(model);

            var mevcut = await _context.Projeler
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.ProjeId == id);
            if (mevcut == null) return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!User.IsInRole("Admin") && mevcut.KullaniciId != userId)
                return Forbid();

            // Sahip bilgisini koru
            model.KullaniciId = mevcut.KullaniciId;

            _context.Projeler.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Proje/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var proje = await _context.Projeler.FindAsync(id);
            if (proje == null) return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!User.IsInRole("Admin") && proje.KullaniciId != userId)
                return Forbid();

            return View(proje);
        }

        // POST: /Proje/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proje = await _context.Projeler.FindAsync(id);
            if (proje != null)
            {
                _context.Projeler.Remove(proje);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
