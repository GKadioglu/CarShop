using System;
using System.Threading.Tasks;
using CarShop.Business.Concrete;
using CarShop.API.Identity;
using CarShop.Entity.Models;
using CarShop.API.EmailServices;
using Core.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly TokenService _tokenService;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IEmailSender emailSender,
            TokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _tokenService = tokenService;

        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return Ok("Kayıt endpointi çalışıyor.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // ModelState doğrulaması
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { success = false, errors });
            }

            // Kullanıcı oluşturma
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Email doğrulama token'ı oluştur
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedCode = Uri.EscapeDataString(code); // Token'ı güvenli şekilde encode et

                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = encodedCode
                }, Request.Scheme);

                try
                {
                    // Email gönder
                    await _emailSender.SendEmailAsync(
                        model.Email,
                        "Hesabınızı Onaylayın",
                        $"Lütfen email hesabınızı onaylamak için <a href='{url}'>buraya tıklayın</a>."
                    );
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = "Email gönderiminde bir hata oluştu.", error = ex.Message });
                }


                return Ok(new { success = true, message = "Kayıt başarılı! Lütfen emailinizi kontrol edin." });

            }

            // Hataları geri döndür
            var error = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { success = false, error });
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest(new { success = false, message = "Geçersiz istek." });
            }

            // Kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            // Token ile kullanıcıyı doğrula
            var result = await _userManager.ConfirmEmailAsync(user, Uri.UnescapeDataString(token));

            // Frontend'in çalıştığı base URL
            var baseUrl = "http://localhost:3000";  // React'in çalıştığı URL

            // Yönlendirme yapılacak URL
            var redirectUrl = result.Succeeded
                ? $"{baseUrl}/confirmedemail?status=success"  // Başarı durumu
                : $"{baseUrl}/confirmedemail?status=failure"; // Başarısız durum

            return Redirect(redirectUrl);  // Yönlendirme yapılır
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            // ReturnUrl parametresi kaldırıldı, direkt ana sayfaya yönlendirecek.
            return Ok(new LoginModel());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Geçersiz giriş bilgileri." });
            }

            // Kullanıcıyı UserManager ile bul
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı." });
            }

            // Kullanıcı giriş işlemi için SignInManager kullanılır
            var result = await _signInManager.PasswordSignInAsync(
                model.UserName, // Kullanıcı adı
                model.Password, // Şifre
                model.RememberMe, // Beni Hatırla seçeneği
                true
            );

            if (result.Succeeded)
            {
                // Kullanıcının rolünü alın
                var role = await _userManager.GetRolesAsync(user); // UserManager ile rolü alın
                var token = await _tokenService.GenerateJwtToken(user.UserName, role.FirstOrDefault()); // Token'ı role ile oluştur

                // Giriş başarılı ve token dönülüyor
                return Ok(new { token, userName = user.UserName, role = role.FirstOrDefault(), email = user.Email });
            }
            else if (result.IsLockedOut)
            {
                // Kullanıcı kilitlenmiş
                return Forbid("Hesabınız geçici olarak kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
            }
            else
            {
                // Geçersiz giriş bilgileri
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı." });
            }
        }
    }
}