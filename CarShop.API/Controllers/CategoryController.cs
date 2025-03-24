using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using CarShop.Entity;
using static CarShop.Entity.Models.CategoryModel;


namespace CarShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("categories")]
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

        [HttpGet("categories/{categoryName}")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest("Kategori adı geçersiz");
            }

            var category = _categoryService.GetCategoryByName(categoryName);

            if (category == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            return Ok(category);
        }

    }
}