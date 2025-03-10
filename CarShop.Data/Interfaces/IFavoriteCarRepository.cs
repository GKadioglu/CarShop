using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface IFavoriteCarRepository : IRepository<FavoriteCar>
    {

        Task AddFavoriteCar(string userName, int carId);
        Task<List<Car>> GetFavoriteCars(string userName);
        Task RemoveFavoriteCar(string userName, int carId); 


    }
}