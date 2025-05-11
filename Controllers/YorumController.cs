using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Controllers
{
    [Authorize]
    public class YorumController : Controller
    {
        private readonly AppDbContext _context;

        public YorumController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Yorum?gorevId=5
        public async Task<IActionResult> Index(int gorevId)
        {
            ViewBag.GorevId = gorevId;

            var yorumlar = await _context.Yorumlar
                .Where(y => y.GorevId == gorevId)
                .OrderByDescending(y => y.OlusturmaTarihi)
                .ToListAsync();

            return View(yorumlar);
        }

        // GET: /Yorum/Create?gorevId=5
        public IActionResult Create(int gorevId)
        {
            ViewBag.GorevId = gorevId;
            return View(new Yorum { GorevId = gorevId });
        }

        // POST: /Yorum/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Yorum model)
        {
            // Zorunlu alan atamaları
            model.KullaniciAdi = User.Identity?.Name ?? "Anonim";
            model.OlusturmaTarihi = DateTime.Now;

            // Navigation property validasyonunu kaldır
            ModelState.Remove(nameof(model.Gorev));

            if (!ModelState.IsValid)
            {
                ViewBag.GorevId = model.GorevId;
                return View(model);
            }

            _context.Yorumlar.Add(model);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { gorevId = model.GorevId });
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {msg}");
                ViewBag.GorevId = model.GorevId;
                return View(model);
            }
        }

        // GET: /Yorum/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum == null) return NotFound();
            return View(yorum);
        }

        // POST: /Yorum/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Yorum model)
        {
            if (id != model.Id) return BadRequest();

            ModelState.Remove(nameof(model.Gorev));
            ModelState.Remove(nameof(model.KullaniciAdi));
            ModelState.Remove(nameof(model.OlusturmaTarihi));

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _context.Yorumlar.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { gorevId = model.GorevId });
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Bu yorum başka bir kullanıcı tarafından değiştirildi.");
                return View(model);
            }
        }

        // GET: /Yorum/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum == null) return NotFound();
            return View(yorum);
        }

        // POST: /Yorum/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum == null) return NotFound();

            _context.Yorumlar.Remove(yorum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { gorevId = yorum.GorevId });
        }
    }
}
