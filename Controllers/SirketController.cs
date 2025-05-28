using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Controllers
{
    public class SirketController : Controller
    {
        private readonly AppDbContext _db;
        public SirketController(AppDbContext db) => _db = db;

        // GET: /Sirket
        public IActionResult Index()
        {
            var list = _db.Sirketler
                          .OrderByDescending(s => s.OlusturmaTarihi)
                          .ToList();
            return View(list);
        }

        // GET: /Sirket/Create
        public IActionResult Create() => View();

        // POST: /Sirket/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Sirket sirket)
        {
            if (!ModelState.IsValid)
                return View(sirket);

            _db.Sirketler.Add(sirket);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Sirket/Edit/5
        public IActionResult Edit(int id)
        {
            var sirket = _db.Sirketler.Find(id);
            if (sirket == null) return NotFound();
            return View(sirket);
        }

        // POST: /Sirket/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Sirket sirket)
        {
            if (!ModelState.IsValid)
                return View(sirket);

            _db.Sirketler.Update(sirket);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Sirket/Delete/5
        public IActionResult Delete(int id)
        {
            var sirket = _db.Sirketler.Find(id);
            if (sirket == null) return NotFound();
            return View(sirket);
        }

        // POST: /Sirket/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sirket = _db.Sirketler.Find(id);
            if (sirket == null) return NotFound();

            _db.Sirketler.Remove(sirket);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
