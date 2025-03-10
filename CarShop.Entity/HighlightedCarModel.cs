using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class HighlightedCarModel: IEntity
    {
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int HighlightedCarId { get; set; }
        public HighlightedCar HighlightedCar { get; set; }
    }
}