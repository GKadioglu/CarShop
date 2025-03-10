using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;

namespace CarShop.Entity.Models
{
    public class CarDetailModel
    {
        public int CarId { get; set; }
        public string Brand { get; set; }  // Marka
        public string Model { get; set; }  // Model
        public int Year { get; set; }     // Yıl
        public decimal Price { get; set; } // Fiyat
        public string? ImageUrl { get; set; }  // Resim URL'si
        public List<CarModelDetail> Models { get; set; }  // İlişkili modeller
        public List<CarCategoryModelDetail> Categories { get; set; }
        public List<CarFavoriteModel> CarFavoriteModels {get; set;}
    }
    public class CarFavoriteModel
    {
        public int FavoriteId { get; set; }
        public string UserName { get; set; }
    }

    public class CarModelDetail
    {
        public int ModelId { get; set; }  // Modelin ID'si
        public string Name { get; set; }  // Model adı
        public string Origin { get; set; }  // Üretim menşei
        public int EstablishmentYear { get; set; }  // Kuruluş yılı
        public string Founder { get; set; }  // Kurucu adı
    }

    public class CarCategoryModelDetail
    {
        public int CategoryId { get; set; }        // Kategorinin benzersiz ID'si
        public string Name { get; set; }    // Kategori adı (örneğin: "SUV", "Jeep", "Ağır Vasıta")
    }
}