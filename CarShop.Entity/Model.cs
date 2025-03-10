using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class Model: IEntity
    {
        
        public int ModelId { get; set; }  // Modelin benzersiz ID'si

        public string Name { get; set; }  // Model adı (örneğin: "Ford Mustang")

    
        public string Origin { get; set; }  // Üretim menşei
        public int EstablishmentYear { get; set; }  // Firmanın kurulum yılı
        public string Founder { get; set; }  // Kurucu bilgisi

        public List<CarModel> CarModels {get; set;}
        public List<HighlightedCarModel> highlightedCarModels {get; set;}
    }
}