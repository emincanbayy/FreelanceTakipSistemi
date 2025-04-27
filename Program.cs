using System;
using Microsoft.AspNetCore.Authentication.Cookies;  // Cookie tabanlı giriş
using Microsoft.EntityFrameworkCore;  // EF Core veritabanı işlemleri
using FreelanceTakipSistemi.Data;  // AppDbContext burada

var builder = WebApplication.CreateBuilder(args);

// 🔗 1. Veritabanı bağlantısı (EF Core + SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔐 2. Cookie tabanlı kimlik doğrulama ayarları
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Kullanici/Giris";  // Giriş yapmayanlar buraya yönlendirilecek
        options.AccessDeniedPath = "/Kullanici/Giris";  // Yetkisiz erişimlerde de buraya yönlendirilecek
    });

// 🧱 3. MVC Controller + View desteği
builder.Services.AddControllersWithViews();

// ⚙️ 4. Uygulama oluşturuluyor
var app = builder.Build();

// 🔧 5. Ortama göre hata sayfası veya geliştirici istisna sayfası
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Hata durumunda gösterilecek sayfa
    app.UseHsts();  // Güvenlik için tarayıcıya HTTPS zorlaması
}

// 🌐 6. HTTP istek yaşam döngüsü ayarları
app.UseHttpsRedirection();  // HTTP → HTTPS yönlendirmesi
app.UseStaticFiles();  // wwwroot içindeki dosyaları sun

app.UseRouting();  // Routing sistemi aktif

app.UseAuthentication();  // Kullanıcı oturum kontrolü
app.UseAuthorization();  // Rol/yetki kontrolü

// 🚪 7. Varsayılan rota (Giriş yapılmışsa buradan başlar)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ▶️ 8. Uygulamayı başlat
app.Run();
