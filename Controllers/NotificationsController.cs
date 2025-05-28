// Controllers/NotificationsController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;               // ← Bu using bildirimi şart
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Data;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly AppDbContext _db;
        public NotificationsController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Notifications
        public async Task<IActionResult> Index()
        {
            // ① Claim'ten UserId okuma (metodun içinde, sınıfın dışında değil)
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int currentUserId))
            {
                // Oturum yoksa / parse edilemediyse login sayfasına yönlendir
                return RedirectToAction("Login", "Account");
            }

            // ② Bildirimleri filtrele ve listele
            var items = await _db.Notifications
                                 .Include(n => n.Gorev)
                                 .Where(n => n.KullaniciId == currentUserId)
                                 .OrderByDescending(n => n.CreatedAt)
                                 .ToListAsync();

            return View(items);
        }

        // POST: /Notifications/MarkAsRead/5
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            // Tekrar Claim okuma (güvenlik için)
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int currentUserId))
                return RedirectToAction("Login", "Account");

            var note = await _db.Notifications.FindAsync(id);
            if (note != null && note.KullaniciId == currentUserId)
            {
                note.IsRead = true;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
