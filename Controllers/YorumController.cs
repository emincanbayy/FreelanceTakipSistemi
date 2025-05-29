using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
        // Yorumları herkese göster
        [AllowAnonymous]
        public IActionResult Index(int gorevId)
        {
            ViewBag.GorevId = gorevId;
            var yorumlar = _context.Yorumlar
                .Where(y => y.GorevId == gorevId)
                .OrderByDescending(y => y.OlusturmaTarihi)
                .AsNoTracking()
                .ToList();
            return View(yorumlar);
        }

        // GET: /Yorum/Create?gorevId=5
        // Yorum ekleme formu herkese açık
        [AllowAnonymous]
        public IActionResult Create(int? gorevId)
        {
            ViewBag.Gorevler = _context.Gorevler
                .AsNoTracking()
                .OrderBy(g => g.TeslimTarihi)
                .ToList();

            var model = new Yorum { GorevId = gorevId ?? 0 };
            return View(model);
        }

        // POST: /Yorum/Create
        // Yorum kaydetme herkese açık
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Create(Yorum yorum)
        {
            yorum.KullaniciAdi = User.Identity?.IsAuthenticated == true
                ? User.Identity.Name!
                : "Anonim";

            ViewBag.Gorevler = _context.Gorevler
                .AsNoTracking()
                .OrderBy(g => g.TeslimTarihi)
                .ToList();

            // RowVersion vs. modelleri kaldır
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
            return RedirectToAction(nameof(Index), new { gorevId = yorum.GorevId });
        }

        // GET: /Yorum/Edit/5
        // Yorum düzenlemek sadece Admin’e ait
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(int id)
        {
            var yorum = _context.Yorumlar.Find(id);
            if (yorum == null) return NotFound();

            ViewBag.Gorevler = _context.Gorevler
                .AsNoTracking()
                .OrderBy(g => g.TeslimTarihi)
                .ToList();

            return View(yorum);
        }

        // POST: /Yorum/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(int id, Yorum yorum)
        {
            if (id != yorum.Id)
                return BadRequest();

            ViewBag.Gorevler = _context.Gorevler
                .AsNoTracking()
                .OrderBy(g => g.TeslimTarihi)
                .ToList();

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
        // Yorum silmek sadece Admin’e ait
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            var yorum = _context.Yorumlar.Find(id);
            if (yorum == null) return NotFound();
            return View(yorum);
        }

        // POST: /Yorum/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteConfirmed(int id)
        {
            var yorum = _context.Yorumlar.Find(id);
            if (yorum == null) return NotFound();

            var gorevId = yorum.GorevId;
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
