using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceTakipSistemi.Controllers
{
    public class ProjeController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor: Veritabanı bağlantısını alıyoruz
        public ProjeController(AppDbContext context)
        {
            _context = context;  // DbContext üzerinden veritabanına erişim sağlıyoruz
        }

        // GET: /Proje/Index
        public async Task<IActionResult> Index()
        {
            // Veritabanından projeleri çekiyoruz
            var projeler = await _context.Projeler.ToListAsync();
            return View(projeler); // Veritabanındaki projeleri View'a gönderiyoruz
        }

        // GET: /Proje/Yeni
        public IActionResult Yeni()
        {
            return View(); // Yeni proje eklemek için boş formu gösteriyoruz
        }

        // POST: /Proje/Yeni
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF koruması
        public async Task<IActionResult> Yeni(Proje proje)
        {
            if (ModelState.IsValid)
            {
                _context.Projeler.Add(proje); // Veritabanına proje ekliyoruz
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Proje başarıyla eklendi."; // Başarı mesajı
                return RedirectToAction("Index"); // Projeler listesine yönlendir
            }

            // Model geçersizse, kullanıcıya hata mesajı gösteriyoruz
            return View(proje);
        }

        // GET: /Proje/Duzenle/{id}
        public async Task<IActionResult> Duzenle(int id)
        {
            // ID'ye göre proje verisini alıyoruz
            var proje = await _context.Projeler.FindAsync(id);
            if (proje == null)
            {
                return NotFound(); // Proje bulunamazsa 404 döndürüyoruz
            }

            return View(proje); // Projeyi düzenlemek için formu gönderiyoruz
        }

        // POST: /Proje/Duzenle/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, Proje proje)
        {
            if (id != proje.ProjeId)
            {
                return NotFound(); // ID uyuşmazsa 404 döndürüyoruz
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proje); // Veritabanındaki projeyi güncelliyoruz
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Proje başarıyla güncellendi."; // Başarı mesajı
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Projeler.Any(e => e.ProjeId == id))
                    {
                        return NotFound(); // Eğer proje bulunmazsa 404 döndürüyoruz
                    }
                    throw; // Diğer hatalar için
                }
                return RedirectToAction("Index"); // Güncellenen projeyi listeliyoruz
            }

            return View(proje); // Model geçersizse formu yeniden gösteriyoruz
        }

        // GET: /Proje/Sil/{id}
        public async Task<IActionResult> Sil(int id)
        {
            var proje = await _context.Projeler.FindAsync(id);
            if (proje == null)
            {
                return NotFound(); // Eğer proje bulunamazsa 404 döndürüyoruz
            }

            _context.Projeler.Remove(proje); // Projeyi veritabanından siliyoruz
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Proje başarıyla silindi."; // Başarı mesajı
            return RedirectToAction("Index"); // Projeler listesine yönlendiriyoruz
        }
    }
}
