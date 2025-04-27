using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FreelanceTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        // Giri� yapm�� kullan�c�lar i�in ana sayfa
        [Authorize]
        public IActionResult Index()
        {
            // Burada kullan�c�n�n bilgilerini �ekebiliriz (Claimlerden)
            var kullaniciIsmi = User.Identity.Name;  // Kullan�c�n�n ismi, Claim �zerinden al�n�yor

            ViewBag.KullaniciIsmi = kullaniciIsmi;  // Kullan�c� ismini View'da g�stermek i�in

            return View();  // Ana sayfa g�r�n�m�n� d�nd�r�yoruz
        }

        public IActionResult Privacy()
        {
            return View();  // Gizlilik s�zle�mesi gibi sayfalar i�in
        }
    }
}
