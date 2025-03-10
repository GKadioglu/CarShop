using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;

namespace CarShop.Entity.Models
{
    public class HighlightedCarsModel
    {
        public class HighlightedCarListModel 
        {
            public List<HighlightedCar> highlightedCars {get; set;}
        }
    }
}