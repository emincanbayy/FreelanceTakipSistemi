using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceTakipSistemi.Controllers
{
    [Authorize] // Varsayılan: tüm aksiyonlar için oturum gerektirir
    public class KullaniciController : Controller
    {
        private readonly AppDbContext _context;
        public KullaniciController(AppDbContext context) => _context = context;

        // -------------------------------
        //  ADMIN İŞLEMLERİ (CRUD)
        // -------------------------------

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var tumKullanicilar = await _context.Kullanicilar.ToListAsync();
            return View(tumKullanicilar);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Kullanicilar.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
            => View(new Kullanici());

        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kullanici model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Parola = BCrypt.Net.BCrypt.HashPassword(model.Parola);
            if (string.IsNullOrWhiteSpace(model.Rol))
                model.Rol = "Kullanici";

            _context.Kullanicilar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Kullanicilar.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kullanici model)
        {
            if (id != model.KullaniciId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var mevcut = await _context.Kullanicilar
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.KullaniciId == id);
            if (mevcut == null) return NotFound();

            // Parola değişmemişse eski hash'i tut
            if (model.Parola != mevcut.Parola)
            {
                model.Parola = BCrypt.Net.BCrypt.HashPassword(model.Parola);
            }

            _context.Kullanicilar.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Kullanicilar.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete"), Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Kullanicilar.FindAsync(id);
            if (user != null)
            {
                _context.Kullanicilar.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // -------------------------------
        //  KAYIT / GİRİŞ / ÇIKIŞ
        // -------------------------------

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Kayit()
            => View(new KullaniciRegisterViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Kayit(KullaniciRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (await _context.Kullanicilar.AnyAsync(u => u.Email == vm.Email))
            {
                ModelState.AddModelError(nameof(vm.Email), "Bu e-posta zaten kullanımda.");
                return View(vm);
            }

            // Yeni Kullanici entity’si oluştur
            var user = new Kullanici
            {
                Isim = vm.Isim,
                Email = vm.Email,
                Parola = BCrypt.Net.BCrypt.HashPassword(vm.Parola),
                Rol = "Kullanici"
            };

            _context.Kullanicilar.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz.";
            return RedirectToAction(nameof(Giris));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Giris()
            => View(new GirisViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Giris(GirisViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Kullanicilar
                                     .FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Parola, user.Parola))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.KullaniciId.ToString()),
                    new Claim(ClaimTypes.Name, user.Isim),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Rol)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                );
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Hata = "E-posta veya parola hatalı.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Giris));
        }
    }
}
