using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Proje> Projeler { get; set; }
        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        // 🆕 Yeni eklenen tablolar
        public DbSet<Sirket> Sirketler { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
