using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class CarModel: IEntity
    {

        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        
    }
}