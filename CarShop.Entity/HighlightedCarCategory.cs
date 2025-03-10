using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class HighlightedCarCategory: IEntity
    {
        public int HighlightedCarId { get; set; }       
        public HighlightedCar HighlightedCar { get; set; }

        public int CategoryId { get; set; }  
        public Category Category { get; set; }
    }
}