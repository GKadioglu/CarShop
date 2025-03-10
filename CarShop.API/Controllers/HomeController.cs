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

            // GetHomePageCars'dan dönen IDataResult<List<Car>>'i alıyoruz
            var result = _carService.GetHomePageCars();

            // Sonuç başarılı mı diye kontrol ediyoruz
            if (!result.Success)
            {
                return BadRequest(result.Message); // Başarısızsa hata mesajını döndürüyoruz
            }

            // Tüm araçları sıralayıp listeye alıyoruz (result.Data ile veri alıyoruz)
            var allCars = result.Data
                                  .OrderBy(car => car.CarId) // ID'ye göre sıralama
                                  .ToList();

            // Toplam araç sayısını alıyoruz
            var totalCars = allCars.Count;

            // Sayfalama işlemi
            var cars = allCars
                .Skip((page - 1) * pageSize) // Sayfa atlama
                .Take(pageSize) // Sayfa başına düşen kayıt sayısı
                .ToList();

            // ViewModel'i dolduruyoruz
            var carViewModel = new CarListModel
            {
                Cars = cars,
                TotalCars = totalCars, // Toplam araç sayısını döndür
                CurrentPage = page,    // Şu anki sayfa
                TotalPages = (int)Math.Ceiling((double)totalCars / pageSize) // Toplam sayfa sayısı
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