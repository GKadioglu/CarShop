using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CarShop.Entity.obj;
using Core.Entities;

namespace CarShop.Entity
{
    public class Car : IEntity
    {
        public int CarId { get; set; }
        public string Brand { get; set; }  // Marka
        public string Model { get; set; } // Model
        public int Year { get; set; }     // Yıl
        public decimal Price { get; set; } // Fiyat
        public string? ImageUrl { get; set; }
        public List<CarModel> CarModels { get; set; }
        public List<CarCategory> CarCategories { get; set; }
        public List<CarCarGallery> CarCarGalleries { get; set; }
        public List<FavoriteCarCar> FavoriteCarCars { get; set; }
        public List<AnonimMessageCar> AnonimMessageCars { get; set; }
        public List<Model3DCar> Model3DCars { get; set; }
        public List<MakeAnOfferCar> MakeAnOfferCars { get; set; }


    }
}