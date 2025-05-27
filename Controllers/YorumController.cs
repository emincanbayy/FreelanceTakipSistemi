using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Controllers
{
    public class YorumController : Controller
    {
        private readonly AppDbContext _context;
        public YorumController(AppDbContext context) => _context = context;

        // GET: /Yorum?gorevId=5
        public IActionResult Index(int gorevId)
        {
            ViewBag.GorevId = gorevId;
            var yorumlar = _context.Yorumlar
                .Where(y => y.GorevId == gorevId)
                .OrderByDescending(y => y.OlusturmaTarihi)
                .ToList();
            return View(yorumlar);
        }

        // GET: /Yorum/Create?gorevId=5
        public IActionResult Create(int? gorevId)
        {
            ViewBag.Gorevler = _context.Gorevler
                .AsNoTracking()
                .OrderBy(g => g.TeslimTarihi)
                .Select(g => new { g.Id, g.Baslik })
                .ToList();

            var model = new Yorum { GorevId = gorevId ?? 0 };
            return View(model);
        }

        // POST: /Yorum/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Yorum yorum)
        {
            // Sistemden kullanıcı adını al
            yorum.KullaniciAdi = User.Identity?.IsAuthenticated == true
                ? User.Identity.Name!
                : "Anonim";

            // Görev listesini yeniden yükle
            ViewBag.Gorevler = _context.Gorevler
                .AsNoTracking()
                .OrderBy(g => g.TeslimTarihi)
                .Select(g => new { g.Id, g.Baslik })
                .ToList();

            // Geçerli bir görev seçildi mi?
            if (!_context.Gorevler.Any(g => g.Id == yorum.GorevId))
                ModelState.AddModelError(nameof(yorum.GorevId), "Lütfen listeden geçerli bir görev seçin.");

            // Bind edilmeyen alanları temizle
            ModelState.Remove(nameof(yorum.KullaniciAdi));
            ModelState.Remove(nameof(yorum.RowVersion));
            ModelState.Remove(nameof(yorum.Gorev));

            if (!ModelState.IsValid)
                return View(yorum);

            yorum.OlusturmaTarihi = DateTime.Now;
            _context.Yorumlar.Add(yorum);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Veritabanı hatası: {ex.InnerException?.Message ?? ex.Message}");
                return View(yorum);
            }

            TempData["Success"] = "Yorum başarıyla kaydedildi.";
            // Yorum eklendikten sonra Index sayfasına yönlendir
            return RedirectToAction(nameof(Index), new { gorevId = yorum.GorevId });
        }

        // GET: /Yorum/Edit/5
        public IActionResult Edit(int id)
        {
            var yorum = _context.Yorumlar.Find(id);
            if (yorum == null) return NotFound();
            return View(yorum);
        }

        // POST: /Yorum/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Yorum yorum)
        {
            // Bind edilmeyen alanları temizle
            ModelState.Remove(nameof(yorum.KullaniciAdi));
            ModelState.Remove(nameof(yorum.RowVersion));
            ModelState.Remove(nameof(yorum.Gorev));

            if (!ModelState.IsValid)
                return View(yorum);

            try
            {
                _context.Yorumlar.Update(yorum);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Güncelleme hatası: {ex.InnerException?.Message ?? ex.Message}");
                return View(yorum);
            }

            return RedirectToAction("Details", "Gorev", new { id = yorum.GorevId });
        }

        // GET: /Yorum/Delete/5
        public IActionResult Delete(int id)
        {
            var yorum = _context.Yorumlar.Find(id);
            if (yorum == null) return NotFound();
            return View(yorum);
        }

        // POST: /Yorum/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var yorum = _context.Yorumlar.Find(id);
            if (yorum == null) return NotFound();

            int gorevId = yorum.GorevId;
            _context.Yorumlar.Remove(yorum);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = $"Silme hatası: {ex.InnerException?.Message ?? ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction("Details", "Gorev", new { id = gorevId });
        }
    }
}
