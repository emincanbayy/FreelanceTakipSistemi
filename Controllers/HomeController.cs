using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;
using Microsoft.AspNetCore.Authorization;
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

        // GET: /
        public async Task<IActionResult> Index()
        {
            var sirketler = await _context.Sirketler
                .AsNoTracking()
                .OrderBy(s => s.Ad)
                .ToListAsync();

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

        // GET: /Home/AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            Response.StatusCode = StatusCodes.Status403Forbidden;
            return View();
        }
    }
}
