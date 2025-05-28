using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            // 1?? Þirketleri ViewBag ile taþý
            ViewBag.Sirketler = _db.Sirketler
                                   .OrderBy(s => s.Ad)
                                   .ToList();

            // 2?? Okunmamýþ bildirim sayýsýný taþý (oturum varsa)
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdStr, out var userId))
            {
                ViewBag.UnreadNotificationCount = _db.Notifications
                                                     .Count(n => n.KullaniciId == userId && !n.IsRead);
            }
            else
            {
                ViewBag.UnreadNotificationCount = 0;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
