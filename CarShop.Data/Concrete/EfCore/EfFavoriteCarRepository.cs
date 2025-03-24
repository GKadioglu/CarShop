using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Data.Abstract;
using CarShop.Entity;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data.Concrete.EfCore
{
    public class EfFavoriteCarRepository : EfCoreGenericRepository<FavoriteCar>, IFavoriteCarRepository
    {
        public EfFavoriteCarRepository(ShopContext context) : base(context)
        {
        }

        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public async Task AddFavoriteCar(string userName, int carId)
        {
            var existingFavorite = await ShopContext.FavoriteCars
                .FirstOrDefaultAsync(f => f.UserName == userName && f.FavoriteId == carId); 

            if (existingFavorite == null)
            {
                existingFavorite = new FavoriteCar
                {
                    FavoriteId = carId, 
                    UserName = userName
                };

                ShopContext.FavoriteCars.Add(existingFavorite);
                await ShopContext.SaveChangesAsync(); 

                var isAlreadyFavorite = await ShopContext.FavoriteCarCars
                    .AnyAsync(fc => fc.FavoriteId == existingFavorite.FavoriteId && fc.CarId == carId);

                if (!isAlreadyFavorite)
                {
                    ShopContext.FavoriteCarCars.Add(new FavoriteCarCar
                    {
                        FavoriteId = existingFavorite.FavoriteId,
                        CarId = carId
                    });

                    await ShopContext.SaveChangesAsync();  
                }
            }
            else
            {
                var isAlreadyFavorite = await ShopContext.FavoriteCarCars
                    .AnyAsync(fc => fc.FavoriteId == existingFavorite.FavoriteId && fc.CarId == carId);

                if (!isAlreadyFavorite)
                {
                    ShopContext.FavoriteCarCars.Add(new FavoriteCarCar
                    {
                        FavoriteId = existingFavorite.FavoriteId,
                        CarId = carId
                    });

                    await ShopContext.SaveChangesAsync();  
                }
            }
        }

        public async Task<List<Car>> GetFavoriteCars(string userName)
        {
            var favoriteCars = await ShopContext.FavoriteCarCars
                .Where(fc => fc.FavoriteCar.UserName == userName) 
                .Include(fc => fc.Car) 
                .Select(fc => fc.Car) 
                .ToListAsync();

            return favoriteCars ?? new List<Car>();
        }
        public async Task RemoveFavoriteCar(string userName, int carId)
        {
            var favoriteCar = await ShopContext.FavoriteCars
                .FirstOrDefaultAsync(f => f.UserName == userName && f.FavoriteId == carId); 

            if (favoriteCar != null)
            {
                var favoriteCarCar = await ShopContext.FavoriteCarCars
                    .FirstOrDefaultAsync(fc => fc.FavoriteId == favoriteCar.FavoriteId && fc.CarId == carId);

                if (favoriteCarCar != null)
                {
                    ShopContext.FavoriteCarCars.Remove(favoriteCarCar);
                    await ShopContext.SaveChangesAsync();  // İlişkiyi kaydet

                    var relatedFavoriteCarCount = await ShopContext.FavoriteCarCars
                        .CountAsync(fc => fc.FavoriteId == favoriteCar.FavoriteId);

                    if (relatedFavoriteCarCount == 0)
                    {
                        ShopContext.FavoriteCars.Remove(favoriteCar);
                        await ShopContext.SaveChangesAsync();  // Favori aracı sil
                    }
                }
            }
            else
            {
                throw new Exception("Favori araç bulunamadı.");
            }
        }
    }
}