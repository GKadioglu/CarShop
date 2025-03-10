using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class FavoriteCar: IEntity
    {
        public int FavoriteId { get; set; }
        public List<FavoriteCarCar> FavoriteCarCars { get; set; }
        public string UserName {get; set;}

    }
}