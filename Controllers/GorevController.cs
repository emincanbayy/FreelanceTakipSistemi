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

        public GorevController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Gorev
        public IActionResult Index()
        {
            var gorevler = _context.Gorevler
                .Include(g => g.Proje)
                .OrderByDescending(g => g.OlusturmaTarihi)
                .ToList();

            return View(gorevler);
        }

        // GET: /Gorev/Details/5
        public IActionResult Details(int id)
        {
            var gorev = _context.Gorevler
                .Include(g => g.Proje)
                .Include(g => g.Yorumlar)
                .FirstOrDefault(g => g.Id == id);

            if (gorev == null)
                return NotFound();

            return View(gorev);
        }

        // GET: /Gorev/Create
        public IActionResult Create()
        {
            ViewBag.ProjeList = new SelectList(_context.Projeler.ToList(), "ProjeId", "Baslik");
            return View(new Gorev());
        }

        // POST: /Gorev/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gorev gorev)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProjeList = new SelectList(_context.Projeler.ToList(), "ProjeId", "Baslik");
                return View(gorev);
            }

            gorev.OlusturmaTarihi = DateTime.Now;
            _context.Gorevler.Add(gorev);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Gorev/Edit/5
        public IActionResult Edit(int id)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev == null)
                return NotFound();

            ViewBag.ProjeList = new SelectList(_context.Projeler.ToList(), "ProjeId", "Baslik", gorev.ProjeId);
            return View(gorev);
        }

        // POST: /Gorev/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gorev gorev)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProjeList = new SelectList(_context.Projeler.ToList(), "ProjeId", "Baslik", gorev.ProjeId);
                return View(gorev);
            }

            _context.Gorevler.Update(gorev);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Gorev/Delete/5
        public IActionResult Delete(int id)
        {
            var gorev = _context.Gorevler
                .Include(g => g.Proje)
                .FirstOrDefault(g => g.Id == id);

            if (gorev == null)
                return NotFound();

            return View(gorev);
        }

        // POST: /Gorev/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev == null)
                return NotFound();

            _context.Gorevler.Remove(gorev);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
