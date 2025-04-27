using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FreelanceTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        // Giriþ yapmýþ kullanýcýlar için ana sayfa
        [Authorize]
        public IActionResult Index()
        {
            // Burada kullanýcýnýn bilgilerini çekebiliriz (Claimlerden)
            var kullaniciIsmi = User.Identity.Name;  // Kullanýcýnýn ismi, Claim üzerinden alýnýyor

            ViewBag.KullaniciIsmi = kullaniciIsmi;  // Kullanýcý ismini View'da göstermek için

            return View();  // Ana sayfa görünümünü döndürüyoruz
        }

        public IActionResult Privacy()
        {
            return View();  // Gizlilik sözleþmesi gibi sayfalar için
        }
    }
}
