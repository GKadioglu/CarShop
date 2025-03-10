using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;

namespace CarShop.Entity.ViewModels
{
    public class CarViewModel
    {


        public class CarListModel
        {
            public List<Car> Cars { get; set; }
            public int TotalCars { get; set; }    // Toplam araç sayısı
            public int CurrentPage { get; set; } // Şu anki sayfa
            public int TotalPages { get; set; }  // Toplam sayfa sayısı
        }

    }
}