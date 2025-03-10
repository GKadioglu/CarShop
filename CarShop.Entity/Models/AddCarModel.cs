using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity.Models
{
    public class AddCarModel
    {
        public string Brand { get; set; } // Marka
        public string Model { get; set; } // Model
        public decimal Price { get; set; } // Fiyat
        public int Year { get; set; } // Yıl
        public int CategoryId { get; set; } // Kategori ID'si
        public int ModelId { get; set; } // Model ID'si
    }
}