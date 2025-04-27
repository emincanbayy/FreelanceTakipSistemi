using FreelanceTakipSistemi.Data; // AppDbContext doğru import edilmeli
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTakipSistemi.Controllers
{
    public class GorevController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor: Veritabanı bağlantısını alıyoruz
        public GorevController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Gorev/Index
        public async Task<IActionResult> Index()
        {
            // Veritabanından tüm görevleri alıyoruz ve projeleri dahil ediyoruz
            var gorevler = await _context.Gorevler.Include(g => g.Proje).ToListAsync();
            return View(gorevler); // Tüm görevleri view'a gönderiyoruz
        }

        // GET: /Gorev/Yeni
        public IActionResult Yeni()
        {
            // Projeleri ViewBag ile gönderiyoruz, yeni görev eklemek için formu gösteriyoruz
            ViewBag.Projeler = _context.Projeler.ToList();
            return View();
        }

        // POST: /Gorev/Yeni
        [HttpPost]
        [ValidateAntiForgeryToken] // Güvenlik için CSRF koruması
        public async Task<IActionResult> Yeni(Gorev gorev)
        {
            // Model geçerliyse veritabanına kaydet
            if (ModelState.IsValid)
            {
                _context.Gorevler.Add(gorev);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Görev başarıyla eklendi."; // Başarı mesajı
                return RedirectToAction("Index"); // Yönlendirme
            }

            // Model geçerli değilse, projeleri yeniden gönderiyoruz ve formu tekrar gösteriyoruz
            ViewBag.Projeler = _context.Projeler.ToList();
            return View(gorev);
        }

        // GET: /Gorev/Duzenle/{id}
        public async Task<IActionResult> Duzenle(int id)
        {
            // Veritabanından ilgili görevi ve ilişkili projeleri alıyoruz
            var gorev = await _context.Gorevler.Include(g => g.Proje).FirstOrDefaultAsync(g => g.GorevId == id);
            if (gorev == null)
            {
                return NotFound(); // Eğer görev bulunamazsa, 404 döndür
            }

            // Projeleri ViewBag ile gönderiyoruz
            ViewBag.Projeler = _context.Projeler.ToList();
            return View(gorev); // Görevi düzenlemek için formu göster
        }

        // POST: /Gorev/Duzenle/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, Gorev gorev)
        {
            if (id != gorev.GorevId)
            {
                return NotFound(); // Eğer görev ID'si uyuşmazsa, 404 döndür
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gorev); // Proje verilerini güncelliyoruz
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Görev başarıyla güncellendi."; // Başarı mesajı
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Veri tabanı güncellenemediğinde hata
                    if (!_context.Gorevler.Any(e => e.GorevId == id))
                    {
                        return NotFound(); // Veritabanında görev bulunamazsa, 404 döndür
                    }
                    throw;
                }
                return RedirectToAction("Index"); // Yönlendirme
            }

            // Hata durumunda projeleri yeniden gönderiyoruz ve formu tekrar gösteriyoruz
            ViewBag.Projeler = _context.Projeler.ToList();
            return View(gorev);
        }
    }
}
