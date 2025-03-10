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
            // Kullanıcının mevcut favori aracını kontrol et
            var existingFavorite = await ShopContext.FavoriteCars
                .FirstOrDefaultAsync(f => f.UserName == userName && f.FavoriteId == carId); // Kullanıcı ve carId'ye göre kontrol et

            // Eğer böyle bir favori kaydı yoksa, yeni bir favori kaydı oluştur
            if (existingFavorite == null)
            {
                // Yeni favori kaydını oluştur
                existingFavorite = new FavoriteCar
                {
                    FavoriteId = carId, // carId'yi FavoriteId olarak ata
                    UserName = userName
                };

                // Yeni favori kaydını FavoriteCars tablosuna ekle
                ShopContext.FavoriteCars.Add(existingFavorite);
                await ShopContext.SaveChangesAsync(); // Kaydet

                // `FavoriteCarCars` tablosunda bu favori ile ilişkili bir araç var mı diye kontrol et
                var isAlreadyFavorite = await ShopContext.FavoriteCarCars
                    .AnyAsync(fc => fc.FavoriteId == existingFavorite.FavoriteId && fc.CarId == carId);

                if (!isAlreadyFavorite)
                {
                    // Eğer bu ilişki yoksa, favori ilişkisini ekle
                    ShopContext.FavoriteCarCars.Add(new FavoriteCarCar
                    {
                        FavoriteId = existingFavorite.FavoriteId,
                        CarId = carId
                    });

                    await ShopContext.SaveChangesAsync();  // Yeni ilişkiyi kaydet
                }
            }
            else
            {
                // Eğer favori zaten varsa, ilişkiyi kontrol et
                var isAlreadyFavorite = await ShopContext.FavoriteCarCars
                    .AnyAsync(fc => fc.FavoriteId == existingFavorite.FavoriteId && fc.CarId == carId);

                if (!isAlreadyFavorite)
                {
                    // Eğer bu ilişki yoksa, favori ilişkisini ekle
                    ShopContext.FavoriteCarCars.Add(new FavoriteCarCar
                    {
                        FavoriteId = existingFavorite.FavoriteId,
                        CarId = carId
                    });

                    await ShopContext.SaveChangesAsync();  // Yeni ilişkiyi kaydet
                }
            }
        }

        public async Task<List<Car>> GetFavoriteCars(string userName)
        {
            // Tek sorgu ile kullanıcıya ait favori araçları getiriyoruz
            var favoriteCars = await ShopContext.FavoriteCarCars
                .Where(fc => fc.FavoriteCar.UserName == userName) // Favori araçları filtrele
                .Include(fc => fc.Car) // Car tablosuyla ilişkilendir
                .Select(fc => fc.Car) // Sadece araç bilgilerini seç
                .ToListAsync();

            return favoriteCars ?? new List<Car>();
        }
        public async Task RemoveFavoriteCar(string userName, int carId)
        {
            // Kullanıcının favori aracını bul
            var favoriteCar = await ShopContext.FavoriteCars
                .FirstOrDefaultAsync(f => f.UserName == userName && f.FavoriteId == carId); // Kullanıcı ve carId'ye göre kontrol et

            // Eğer favori araç bulunmuşsa
            if (favoriteCar != null)
            {
                // Favori ilişkisinin FavoriteCarCars tablosunda var olup olmadığını kontrol et
                var favoriteCarCar = await ShopContext.FavoriteCarCars
                    .FirstOrDefaultAsync(fc => fc.FavoriteId == favoriteCar.FavoriteId && fc.CarId == carId);

                if (favoriteCarCar != null)
                {
                    // Favori araç ilişkisini sil
                    ShopContext.FavoriteCarCars.Remove(favoriteCarCar);
                    await ShopContext.SaveChangesAsync();  // İlişkiyi kaydet

                    // Eğer bu favori araç artık hiçbir kullanıcı tarafından favoriye eklenmemişse, FavoriteCars tablosundan da sil
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
                // Eğer böyle bir favori araç yoksa, hata mesajı verebiliriz veya başka bir işlem yapabiliriz
                throw new Exception("Favori araç bulunamadı.");
            }
        }
    }
}