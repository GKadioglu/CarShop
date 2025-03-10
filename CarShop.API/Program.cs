using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CarShop.Data.Concrete;
using CarShop.Business.Abstract;
using CarShop.Data.Abstract;
using CarShop.Data.Concrete.EfCore;
using CarShop.Business.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CarShop.API.EmailServices;
using CarShop.API.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarShop.API.UserProcess.Abstract;
using CarShop.API.UserProcess.Concrete;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using Core.Token;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CarShop.Business.DependencyResolvers.Autofac;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.DependencyResolver;
using log4net;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

// Loglamayı konsola yönlendirin
builder.Logging.AddConsole();  // Konsol loglarını aktif edin

// DbContext'i ve UnitOfWork'ü ekleyin
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

// Identity ayarları
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero // Token zaman farkı sıfırlanır
    };
});

// Identity konfigürasyonu
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

// Cookie ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".ShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddDepedencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

// E-posta servisi
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
    new SmtpEmailSender(
        configuration["EmailSender:Host"],
        configuration.GetValue<int>("EmailSender:Port"),
        configuration.GetValue<bool>("EmailSender:EnableSSL"),
        configuration["EmailSender:UserName"],
        configuration["EmailSender:Password"]
    ));

// UnitOfWork için DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<TokenHelper>();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<NewsApiServiceManager>();

// Servisleri ve Manager'ları ekleyin
builder.Services.AddScoped<ICarService, CarManager>();
builder.Services.AddScoped<IAnonimMessageService, AnonimMessageManager>();
builder.Services.AddScoped<IAdminMessageService, AdminMessageManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IModelService, ModelManager>();
builder.Services.AddScoped<IModel3DService, Model3DManager>();
builder.Services.AddScoped<IUserMakeAnOfferService, UserMakeAnOfferManager>();
builder.Services.AddScoped<IAdminMakeAnOfferService, AdminMakeAnOfferManager>();
builder.Services.AddScoped<IFavoriteCarService, FavoriteCarManager>(); // Burada FavoriteCarManager sınıfını kaydediyoruz

builder.Services.AddControllersWithViews();  // MVC destek için
builder.Services.AddSingleton<TokenService>();

// CORS politikalarını ekleyin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000") // React uygulamasının adresi
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));


var app = builder.Build();
app.Urls.Add("http://localhost:5000");

// Middleware ve HTTP istekleri yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot'taki dosyaları sunmak için



app.ConfigureCustomExceptionMiddleware();
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata oluştu: {ex.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
        throw;
    }
});
app.UseRouting();
app.UseCors("AllowReactApp"); // CORS middleware burada olmalı
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



// Identity Seed işlemi
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedIdentity.Seed(userManager, roleManager, configuration);
}

app.Run();