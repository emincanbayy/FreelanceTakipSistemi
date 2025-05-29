using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceTakipSistemi.Controllers
{
    [Authorize(Policy = "AdminOnly")] // Sadece admin erişebilsin
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Views/Admin/Index.cshtml sayfasını çağırır
        }
    }
}
