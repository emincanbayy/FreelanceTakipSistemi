// Controllers/HomeController.cs
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FreelanceTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: / or /Home/Index
        public async Task<IActionResult> Index()
        {
            // Veritabanýndan þirketleri çek
            var sirketler = await _context.Sirketler
                .AsNoTracking()
                .OrderBy(s => s.Ad)
                .ToListAsync();

            // ViewBag'e ata
            ViewBag.Sirketler = sirketler;
            return View();
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/Error
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
