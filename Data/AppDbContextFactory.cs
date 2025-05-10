using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FreelanceTakipSistemi.Data;

namespace FreelanceTakipSistemi.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            // Buraya uygulamanızın connection string'ini girin:
            // appsettings.json içindekini birebir yazarak da alabilirsiniz.
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FreelanceTakipSistemi;Trusted_Connection=True;");

            return new AppDbContext(builder.Options);
        }
    }
}
