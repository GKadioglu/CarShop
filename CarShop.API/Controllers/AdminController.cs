using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Contants;
using CarShop.Entity.Models;
using CarShop.API.Identity;
using CarShop.API.UserProcess.Abstract;
using CarShop.API.UserProcess.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static CarShop.Entity.Models.CategoryModel;
using static CarShop.Entity.ViewModels.CarViewModel;

namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IModelService _modelService;
        private readonly ICategoryService _categoryService;
        private readonly IAnonimMessageService _anonimMessageService;
        private readonly IAdminMessageService _adminMessageService;
        private readonly IUserMakeAnOfferService _userMakeAnOfferService;
        private readonly IAdminMakeAnOfferService _adminMakeAnOfferService;
        private readonly INotificationService _notificationService;


        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICarService carService, IModelService modelService, ICategoryService categoryService, IUserService userService, IAnonimMessageService anonimMessageService, IAdminMessageService adminMessageService, INotificationService notificationService, IUserMakeAnOfferService userMakeAnOfferService, IAdminMakeAnOfferService adminMakeAnOfferService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _carService = carService;
            _modelService = modelService;
            _categoryService = categoryService;
            _userService = userService;
            _anonimMessageService = anonimMessageService;
            _adminMessageService = adminMessageService;
            _notificationService = notificationService;
            _adminMakeAnOfferService = adminMakeAnOfferService;
            _userMakeAnOfferService = userMakeAnOfferService;

        }

        [HttpGet("allcars")]
        public IActionResult AllCars()
        {
            var allCarsResult = _carService.GetHomePageCars();

            if (allCarsResult.Success)
            {
                var allCars = allCarsResult.Data
                                          .OrderBy(car => car.CarId) // ID'ye göre sıralama
                                          .ToList();

                var carViewModel = new CarListModel
                {
                    Cars = allCars
                };

                return Ok(carViewModel);
            }

            return BadRequest(allCarsResult.Message);
        }

        [HttpDelete("deletecar")]
        public IActionResult DeleteCar(int id)
        {
            if (id == 0)
            {
                return BadRequest("Geçersiz ID");
            }

            var result = _carService.DeleteCar(id);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("getmodels")]
        public IActionResult GetModels([FromQuery] int? Id)
        {
            var models = _modelService.GetModels(Id);
            if (models == null || !models.Any())
            {
                return NotFound("No models found.");
            }
            return Ok(models);
        }

        [HttpGet("getcategories")]
        public IActionResult Category()
        {
            try
            {
                var result = _categoryService.GetAllCategory();

                if (result.Success)
                {
                    var categoryModel = new CategoryListModel
                    {
                        Categories = result.Data 
                    };
                    return Ok(categoryModel);
                }

                return BadRequest(new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kategori bilgileri alınamadı.", error = ex.Message });
            }
        }

        [HttpPost("addnewcar")]
        public IActionResult AddNewCar([FromBody] AddCarModel request)
        {

            var newCar = _carService.AddNewCar(
                request.Brand,
                request.Model,
                request.Price,
                request.Year,
                request.CategoryId,
                request.ModelId
            );

            return Ok(newCar);
        }

        [HttpPost("addNewNotification")]
        public IActionResult AddNewNotification([FromBody] AddNotificationModel request)
        {
            try
            {
                var newNotification = _notificationService.AddNewNotification(
                    request.Sender,
                    request.Title,
                    request.Description,
                    request.CreatedDate
                );

                return Ok(newNotification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();

            if (users == null || !users.Any())
            {
                return NotFound(new { message = "No users found." });
            }

            return Ok(users);
        }

        [HttpGet("getMessage")]
        public IActionResult GetMessage()
        {
            try
            {
                // Anonim mesajları alıyoruz
                var result = _anonimMessageService.GetMessage();

                if (result == null || !result.Data.Any())
                {
                    return NotFound(new { message = result.Message }); 
                }

                return Ok(result.Data); 
            }
            catch (Exception ex)
            {
                // Hata durumu
                return StatusCode(500, new { message = "Mesajlar alınamadı.", error = ex.Message });
            }
        }

        [HttpGet("getUserOffer")]
        public async Task<IActionResult> GetUserOffer()
        {
            try
            {
                // teklifleri alıyoruz
                var result = _userMakeAnOfferService.GetUserOffer();

                if (result == null || !result.Data.Any())
                {
                    return NotFound(new { message = result.Message }); // teklif bulunamadıysa
                }

                return Ok(result.Data); // Veri ile birlikte başarılı dönüş
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Teklif alınamadı.",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpGet("getAdminOffer")]
        public async Task<IActionResult> GetAdminOffer()
        {
            var adminOffers = await _adminMakeAnOfferService.GetAdminOffer();

            if (adminOffers == null || !adminOffers.Success)
            {
                return NotFound(new { Message = "Teklif cevabı bulunamadı." });
            }

            return Ok(adminOffers);
        }

        [HttpPost("replyOffer")]
        public async Task<IActionResult> ReplyOffer([FromBody] ReplyOfferDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _adminMakeAnOfferService.ReplyOfferAsync(dto.Sender, dto.Receiver, dto.Acceptance, dto.UserOfferId);
                if (result.Success)
                {
                    return Ok(new { message = result.Message, data = result.Data });
                }
                return BadRequest(new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("answerMessage")]
        public async Task<IActionResult> AnswerMessage([FromBody] AnswerMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _adminMessageService.AnswerMessageAsync(dto.Sender, dto.Receiver, dto.Message, dto.MessageId);

                if (result.Success)
                {
                    return Ok(new { message = result.Message, data = result.Data });
                }

                return BadRequest(new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        public class AnswerMessageDto
        {
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public string Message { get; set; }
            public int MessageId { get; set; }
        }

    }
}
