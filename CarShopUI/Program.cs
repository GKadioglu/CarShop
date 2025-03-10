using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 🔹 MVC ve Razor Pages Desteği
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 🔹 CORS Politikası (React için API'ye erişim izni)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000") // React geliştirme sunucusunun adresi
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

// 🔹 Geliştirme ortamında hata yakalama
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 🔹 HTTPS yönlendirme ve statik dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// 🔹 CORS middleware (React'ın API'ye erişebilmesi için)
app.UseCors("AllowReactApp");

// 🔹 Routing, Authentication ve Authorization
app.UseRouting();
app.UseAuthorization();

// 🔹 React Build Dosyalarını Sunma (Eğer React'ı ASP.NET içinde host ediyorsan)
app.UseDefaultFiles();  // index.html gibi dosyaları otomatik bulur
app.UseStaticFiles();   // wwwroot ve build klasörünü sunar

// 🔹 API ve UI yönlendirme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 🔹 Eğer React SPA Uygulaması Dahilse (SPA Fallback)
app.MapFallbackToFile("/index.html");

app.Run();
