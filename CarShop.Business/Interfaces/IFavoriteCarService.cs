using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.Models;
using CarShop.Entity;

namespace CarShop.Business.Abstract
{
    public interface IFavoriteCarService
    {
        Task AddFavoriteCar(string userName, int carId);
        Task<List<CarDetailModel>> GetFavoriteCars(string userName);  
        Task RemoveFavoriteCar(string userName, int carId);

    }
}