using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FreelanceTakipSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly AppDbContext _context;

        public KullaniciController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Kullanici/Kayit
        public IActionResult Kayit()
        {
            return View();
        }

        // POST: /Kullanici/Kayit
        [HttpPost]
        public async Task<IActionResult> Kayit(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                var varMi = await _context.Kullanicilar.AnyAsync(k => k.Email == model.Email);
                if (varMi)
                {
                    ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                    return View(model);
                }

                model.Parola = BCrypt.Net.BCrypt.HashPassword(model.Parola);
                _context.Kullanicilar.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Giris");
            }

            return View(model);
        }

        // GET: /Kullanici/Giris
        public IActionResult Giris()
        {
            return View();
        }

        // POST: /Kullanici/Giris
        [HttpPost]
        public async Task<IActionResult> Giris(GirisViewModel model)
        {
            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == model.Email);

            if (kullanici != null && BCrypt.Net.BCrypt.Verify(model.Parola, kullanici.Parola))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciId.ToString()),
                    new Claim(ClaimTypes.Name, kullanici.Isim),
                    new Claim(ClaimTypes.Email, kullanici.Email),
                    new Claim(ClaimTypes.Role, kullanici.Rol)
                };

                ClaimsIdentity kimlik = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(kimlik);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Hata = "E-posta veya parola hatalı.";
            return View(model);
        }

        // /Kullanici/Cikis
        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Giris");
        }
    }
}
