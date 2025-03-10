using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class CarGallery: IEntity
    {
        public int CarGalleryId { get; set; }
        public string ImageUrl { get; set; }
        public List<CarCarGallery> CarCarGalleries {get; set;}
    }
}