using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class CarCategory : IEntity
    {
        public int CarId { get; set; }       // Car tablosu ile ilişki (Foreign Key)
        public Car Car { get; set; }

        public int CategoryId { get; set; }  // Category tablosu ile ilişki (Foreign Key)
        public Category Category { get; set; }
    }
}