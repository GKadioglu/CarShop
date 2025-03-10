using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class FavoriteCarCar: IEntity
    {
        public int CarId { get; set; }       // Car tablosu ile ilişki (Foreign Key)
        public Car Car { get; set; }

        public int FavoriteId { get; set; }
        public FavoriteCar FavoriteCar { get; set; }
    }
}