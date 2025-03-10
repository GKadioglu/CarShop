using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class HighlightedCar: IEntity
    {
        public int HighlightedCarId { get; set; }
        public string Brand { get; set; }  // Marka
        public string Model { get; set; } // Model
        public int Year { get; set; }     // Yıl
        public decimal Price { get; set; } // Fiyat
        public string? ImageUrl { get; set; }
        public List<HighlightedCarModel> HighlightedCarModels {get; set;}
        public List<HighlightedCarCategory> HighlightedCarCategories { get; set; }
    }

}