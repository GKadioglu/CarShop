using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly NewsApiServiceManager _newsApiService;

        public BlogController(NewsApiServiceManager newsApiService)
        {
            _newsApiService = newsApiService;
        }

         [HttpGet("latest-news")]
        public async Task<IActionResult> GetLatestCarNews([FromQuery] string query = "car", [FromQuery] string sortBy = "publishedAt", [FromQuery] int page = 1)
        {
            try
            {
                var news = await _newsApiService.GetLatestCarNews(query, sortBy, page);
                return Ok(news);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
