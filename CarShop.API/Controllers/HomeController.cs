using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using static CarShop.Entity.Models.CategoryModel;
using static CarShop.Entity.Models.HighlightedCarsModel;
using static CarShop.Entity.ViewModels.CarViewModel;

namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IModelService _modelService;
        private readonly ICategoryService _categoryService;

        public HomeController(ICarService carService, IModelService modelService, ICategoryService categoryService)
        {
            _carService = carService;
            _modelService = modelService;
            _categoryService = categoryService;

        }

        [HttpGet("index")]
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Sayfa veya sayfa boyutu 1'den küçük olamaz.");
            }

            var result = _carService.GetHomePageCars();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var allCars = result.Data
                                  .OrderBy(car => car.CarId) 
                                  .ToList();

            var totalCars = allCars.Count;

            var cars = allCars
                .Skip((page - 1) * pageSize) 
                .Take(pageSize) 
                .ToList();

            var carViewModel = new CarListModel
            {
                Cars = cars,
                TotalCars = totalCars, 
                CurrentPage = page,    
                TotalPages = (int)Math.Ceiling((double)totalCars / pageSize) 
            };

            return Ok(carViewModel);
        }

        [HttpGet("HighlightedCars")]
        public IActionResult HighlightedCars()
        {
            var result = _carService.GetHighlightedCars();

            if (!result.Success)
            {   
                return BadRequest(result.Message); 
            }

            var highlightedCarsModel = new HighlightedCarListModel()
            {
                highlightedCars = result.Data 
            };

            return Ok(highlightedCarsModel);
        }



    }
}