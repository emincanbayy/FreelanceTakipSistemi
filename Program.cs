using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using FreelanceTakipSistemi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. EF Core + SQL Server bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Cookie tabanlı kimlik doğrulama ayarları
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Kullanici/Giris"; // Giriş yapmamış kullanıcılar buraya yönlendirilir
        options.AccessDeniedPath = "/Home/AccessDenied"; // Yetkisi olmayan (örneğin admin olmayan) kullanıcılar buraya yönlendirilir
    });

// 3. Admin policy tanımı
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

// 4. MVC Controller + Razor View desteği
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 5. Hata ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 6. Kimlik ve Yetki kontrol middleware'leri
app.UseAuthentication();
app.UseAuthorization();

// 7. Varsayılan rota
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
