using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class CarCarGallery: IEntity
    {
        public int CarId { get; set; }       // Car tablosu ile ilişki (Foreign Key)
        public Car Car { get; set; }

        public int CarGalleryId { get; set; }
        public CarGallery CarGallery {get; set;}
    }
}