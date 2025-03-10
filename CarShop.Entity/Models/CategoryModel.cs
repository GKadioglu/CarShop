using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;

namespace CarShop.Entity.Models
{
    public class CategoryModel
    {
        public class CategoryListModel
        {
            public List<Category> Categories { get; set; }
        }

        public class GetCategoryByNameModel
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public List<CarCategoryModel> Cars { get; set; }
        }
        public class CarCategoryModel
        {
        public int CarId { get; set; }

        public string Brand { get; set; }  // Marka
        public string Model { get; set; }  // Model
        public int Year { get; set; }     // Yıl
        public decimal Price { get; set; } // Fiyat
        public string? ImageUrl { get; set; }  // Resim URL'si
        }

        public class Models 
        {
        public int ModelId { get; set; }  

        public string Name { get; set; }  
        }

    }
}