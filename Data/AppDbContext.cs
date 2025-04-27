using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Models;

namespace FreelanceTakipSistemi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kullanici> Kullanicilar { get; set; }  // Kullanıcılar tablosu
        public DbSet<Proje> Projeler { get; set; }  // Projeler tablosu
        public DbSet<Gorev> Gorevler { get; set; }  // Görevler tablosu
        public DbSet<Yorum> Yorumlar { get; set; }  // Yorumlar tablosu (Varsa)

        // Model oluşturulurken ilişkiler tanımlanıyor
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Proje ile Gorev arasındaki ilişkiyi belirliyoruz (Proje ve Gorev arasında 1:N ilişkisi)
            modelBuilder.Entity<Gorev>()
                .HasOne(g => g.Proje)  // Bir görev sadece bir proje ile ilişkilidir
                .WithMany(p => p.Gorevler) // Bir proje birden fazla göreve sahip olabilir
                .HasForeignKey(g => g.ProjeId);  // ProjeId, Gorev tablosunda dış anahtar

            // Gorev ile Yorum arasındaki ilişkiyi belirliyoruz (Gorev ve Yorum arasında 1:N ilişkisi)
            modelBuilder.Entity<Gorev>()
                .HasMany(g => g.Yorumlar)  // Bir görev birden fazla yoruma sahip olabilir
                .WithOne(y => y.Gorev)     // Her yorum bir görev ile ilişkilidir
                .HasForeignKey(y => y.GorevId);  // Yorum tablosunda GorevId dış anahtar
        }
    }
}
