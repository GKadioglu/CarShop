using CarShop.Business.Abstract;
using CarShop.Entity.Models;
using CarShop.API.UserProcess.Abstract;
using Microsoft.AspNetCore.Mvc;
using static CarShop.Entity.ViewModels.CarViewModel;

namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // API Controller özelliğini ekleyelim
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IModel3DService _model3DService;




        public CarController(ICarService carService, IModel3DService model3DService)
        {
            _carService = carService;
            _model3DService = model3DService;


        }

        [HttpGet("get3dModels")]
        public IActionResult Get3DModels(int id)
        {
            // Parametre kontrolü
            if (id <= 0)
            {
                return BadRequest("Geçersiz araç ID.");
            }

            var model = _model3DService.GetModel3D(id);

            if (model == null)
            {
                return NotFound("Belirtilen araç için 3D model bulunamadı.");
            }

            return Ok(model);
        }


        [HttpGet("details/{id}")]
        public IActionResult GetCarByIdDetails(int id)
        {
            if (id == 0)
            {
                return BadRequest("Geçersiz ID");
            }

            var car = _carService.GetCarByIdDetails(id);
            if (car == null)
            {
                return NotFound("Araç bulunamadı");
            }

            return Ok(car);
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            var searchResult = _carService.GetSearchResult(name);

            if (searchResult.Success)
            {
                var carViewModel = new CarListModel()
                {
                    Cars = searchResult.Data
                };
                return Ok(carViewModel);
            }
            return BadRequest(searchResult.Message);
        }

        [HttpGet("contact/{carName}")]
        public IActionResult Contact(string carName)
        {
            var car = _carService.GetCarByName(carName);

            if (car == null)
                return NotFound("Car not found");
            return Ok(car);
        }
    }
}
