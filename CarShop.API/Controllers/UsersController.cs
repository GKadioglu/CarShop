using CarShop.Business.Abstract;
using CarShop.Business.Concrete;
using CarShop.API.Identity;
using CarShop.Data;
using CarShop.Entity.Models;
using CarShop.API.UserProcess.Abstract;
using Core.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using CarShop.API.UserProcess.UserEdit;

namespace CarShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly TokenService _tokenService;
        private readonly IFavoriteCarService _favoriteCarService;
        private readonly TokenHelper _tokenHelper;
        private readonly IAdminMessageService _adminMessageService;
        private readonly INotificationService _notificationService;
        private readonly IAnonimMessageService _anonimMessageService;
        private readonly IAdminMakeAnOfferService _adminMakeAnOfferService;

        public UsersController(IUserService userService, TokenService tokenService, IFavoriteCarService favoriteCarService, TokenHelper tokenHelper, IAdminMessageService adminMessageService, INotificationService notificationService, IAdminMakeAnOfferService adminMakeAnOfferService, IAnonimMessageService anonimMessageService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _favoriteCarService = favoriteCarService;
            _tokenHelper = tokenHelper;
            _adminMakeAnOfferService = adminMakeAnOfferService;
            _notificationService = notificationService;
            _adminMessageService = adminMessageService;
            _anonimMessageService = anonimMessageService;
        }

        [HttpGet("getAdminMessage")]
        public async Task<IActionResult> GetAdminMessage(string email)
        {
            try
            {
                // Kullanıcıyı al
                var user = await _userService.GetUserByEmail(email);

                if (user == null)
                {
                    return NotFound(new { Message = "Kullanıcı bulunamadı." });
                }

                // Admin mesajını getirme
                var adminMessages = await _adminMessageService.GetAdminMessagesByEmail(email);

                if (adminMessages == null || adminMessages.Data == null || !adminMessages.Data.Any())
                {
                    return NotFound(new { Message = "Mesaj bulunamadı." });
                }

                return Ok(adminMessages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Sunucu hatası oluştu.",
                    Error = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpDelete("deleteMessage/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameFromToken))
                {
                    return Unauthorized(new { message = "Username not found in token" });
                }

                // Mesajı silme işlemini gerçekleştir
                var result = _anonimMessageService.RemoveUserMessage(usernameFromToken, messageId);

                if (!result.Success)
                {
                    return BadRequest(new { message = result.Message });
                }

                return Ok(new { message = result.Message, data = result.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the message.", error = ex.Message });
            }
        }

        [HttpGet("getAdminOffer")]
        public async Task<IActionResult> GetAdminOffer(string email)
        {
            // Kullanıcıyı al
            var user = await _userService.GetUserByEmail(email);

            // Admin mesajını getirme
            var adminOffer = await _adminMakeAnOfferService.GetAdminOfferByEmail(email);
            if (adminOffer == null)
            {
                return NotFound(new { Message = "Teklif cevabı bulunamadı." });
            }

            // Başarılı dönüş
            return Ok(adminOffer);
        }

        [HttpGet("getNotifications")]
        [Authorize]
        public async Task<IActionResult> GetNotifications()
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);
                if (claimsPrincipal == null)
                {
                    return Unauthorized(new { message = "Invalid token" });
                }

                // Kullanıcı adını token'dan alıyoruz
                var userName = claimsPrincipal.Identity.Name;

                // Kullanıcının sadece kendi bildirim bilgilerini al
                var result = _notificationService.GetNotifications(userName);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Notifications not received", error = ex.Message });
            }
        }

        [HttpPost("updateNotifications")]
        public async Task<IActionResult> UpdateNotifications([FromBody] UserNotificationModel request)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameFromToken))
                {
                    return Unauthorized(new { message = "Username not found in token" });
                }

                // Notification güncelleme işlemi
                var updateResult = await _notificationService.UpdateNotificationReadStatusAsync(usernameFromToken, request.NotificationsId, request.Reads);

                if (updateResult.Success)
                {
                    return Ok(new { message = updateResult.Message, data = updateResult.Data });
                }
                else
                {
                    return BadRequest(new { message = updateResult.Message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Notifications not updated", error = ex.Message });
            }
        }



        [HttpGet("getUser/{name}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string name)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                // Kullanıcıyı al
                var user = await _userService.GetUserByIdOrUsernameAsync(usernameFromToken);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                return Ok(user);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = "Token validation failed", error = ex.Message });
            }
        }

        [HttpPut("userEdit")]
        [Authorize]
        public async Task<IActionResult> UserEdit([FromBody] UserEditRequest request)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token içeriği ile kullanıcıyı al
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki email veya username bilgisini al
                var userNameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userNameFromToken))
                {
                    return Unauthorized(new { message = "Username not found in token" });
                }

                // Kullanıcı bilgilerini güncelle
                var result = await _userService.UserEdit(request.FirstName, request.LastName, request.UserName);

                if (!result.Success)
                {
                    return BadRequest(new { message = result.Message });
                }

                return Ok(new { message = result.Message, user = result.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user", error = ex.Message });
            }
        }

        [HttpPost("addFavoriteCar")]
        [Authorize]
        public async Task<IActionResult> AddFavoriteCar([FromBody] FavoriteCarRequest request)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                // Favori aracı ekle
                await _favoriteCarService.AddFavoriteCar(usernameFromToken, request.CarId);

                return Ok(new { message = "Car added to favorites successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to add car to favorites", error = ex.Message });
            }
        }

        public class FavoriteCarRequest
        {
            public int CarId { get; set; } // Dinamik carId burada tanımlanıyor
        }

        [HttpPost("removeFavoriteCar")]
        [Authorize]
        public async Task<IActionResult> RemoveFavoriteCar([FromBody] FavoriteCarRequest request)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                // Favori aracı sil
                await _favoriteCarService.RemoveFavoriteCar(usernameFromToken, request.CarId);

                return Ok(new { message = "Car removed from favorites successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to remove car from favorites", error = ex.Message });
            }
        }

        [HttpDelete("removesFavoriteCar")]
        [Authorize]
        public async Task<IActionResult> RemovesFavoriteCar([FromBody] FavoriteCarRequest request)
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                // Favori aracı sil
                await _favoriteCarService.RemoveFavoriteCar(usernameFromToken, request.CarId);

                return Ok(new { message = "Car removed from favorites successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to remove car from favorites", error = ex.Message });
            }
        }
        [HttpGet("getFavoriteCars")]
        [Authorize]
        public async Task<IActionResult> GetFavoriteCars()
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                // Favori araçları getir
                var favoriteCars = await _favoriteCarService.GetFavoriteCars(usernameFromToken);

                return Ok(favoriteCars);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve favorite cars", error = ex.Message });
            }
        }

        [HttpDelete("deleteUser")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki username'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameFromToken))
                {
                    return BadRequest(new { message = "Kullanıcı adı token'da bulunamadı." });
                }

                // Kullanıcıyı sil
                var deleteUser = await _userService.DeleteUser(usernameFromToken);

                return Ok(new { message = "Kullanıcı başarıyla silindi", user = deleteUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Kullanıcı silinemedi", error = ex.Message });
            }
        }

        [HttpGet("getUserDetail")]
        [Authorize]
        public async Task<IActionResult> GetUserDetail()
        {
            var token = _tokenHelper.GetTokenFromHeader();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Token is missing" });
            }

            try
            {
                // Token'ı çözümle
                var claimsPrincipal = _tokenService.DecodeJwtToken(token);

                // Token içindeki email'i al
                var usernameFromToken = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                // kullanıcı detaylarını getir
                var userDetail = await _userService.GetUserDetail(usernameFromToken);

                return Ok(userDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "User details could not be retrieved", error = ex.Message });
            }
        }
    }
}