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
    public class EfCoreCarRepository : EfCoreGenericRepository<Car>, ICarRepository
    {
        public EfCoreCarRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public Car AddNewCar(string brand, string model, decimal price, int year, int categoryId, int modelId)
        {
            // En son eklenen CarId'yi alıyoruz
            int newCarId = ShopContext.Cars
                                      .OrderByDescending(c => c.CarId)
                                      .FirstOrDefault()?.CarId + 1 ?? 1; // Eğer hiçbir araç yoksa 1'den başla

            // Yeni araba nesnesi oluşturuluyor ve CarId manuel olarak atanıyor
            var newCar = new Car
            {
                CarId = newCarId, // Manuel olarak CarId belirliyoruz
                Brand = brand,
                Model = model,
                Price = price,
                Year = year
            };

            // Yeni arabayı veritabanına ekliyoruz
            ShopContext.Cars.Add(newCar);
            ShopContext.SaveChanges();  // İlk SaveChanges() burada araba kaydediliyor ve CarId oluşuyor.

            // Var olan kategoriyi buluyoruz ya da yeni kategori ekliyoruz
            var existingCategory = ShopContext.CarCategories
                .FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory != null)
            {
                ShopContext.CarCategories.Add(new CarCategory { CarId = newCar.CarId, CategoryId = categoryId });
            }
            else
            {
                ShopContext.CarCategories.Add(new CarCategory { CarId = newCar.CarId, CategoryId = categoryId });
            }

            // Var olan modeli buluyoruz ya da yeni model ekliyoruz
            var existingModel = ShopContext.CarModels
                .FirstOrDefault(m => m.ModelId == modelId);
            if (existingModel != null)
            {
                ShopContext.CarModels.Add(new CarModel { CarId = newCar.CarId, ModelId = modelId });
            }
            else
            {
                ShopContext.CarModels.Add(new CarModel { CarId = newCar.CarId, ModelId = modelId });
            }

            // Son SaveChanges() çağrısını burada yapıyoruz
            ShopContext.SaveChanges();  // Son değişiklikleri kaydediyoruz.

            return newCar;
        }

        public Car DeleteCar(int carId)
        {
            var carToDelete = ShopContext.Cars
                                          .Where(i => i.CarId == carId)
                                          .Include(i => i.CarCategories)
                                          .Include(i => i.CarModels)
                                          .Include(i => i.FavoriteCarCars)
                                          .FirstOrDefault();

            if (carToDelete != null)
            {
                // Favori tablosundan silme işlemi
                ShopContext.FavoriteCarCars.RemoveRange(carToDelete.FavoriteCarCars);

                // Kategori ve model ilişkilerini silme
                ShopContext.CarCategories.RemoveRange(carToDelete.CarCategories);
                ShopContext.CarModels.RemoveRange(carToDelete.CarModels);

                // Araba kaydını silme
                ShopContext.Cars.Remove(carToDelete);

                // Değişiklikleri veritabanına kaydetmek
                ShopContext.SaveChanges();
            }

            return carToDelete;  // Silinen araba bilgisi döndürülüyor (ya da null dönebilir)
        }

        public Car GetCarByName(string carName)
        {
            return ShopContext.Cars
                .Include(c => c.CarCategories).ThenInclude(cc => cc.Category)
                .Include(c => c.CarModels).ThenInclude(cm => cm.Model)
                .FirstOrDefault(c => (c.Brand + "-" + c.Model) == carName);
        }

        public Car GetCarDetails(int id)
        {
            return ShopContext.Cars
                           .Where(i => i.CarId == id)
                           .Include(i => i.CarModels)
                           .ThenInclude(i => i.Model)
                           .FirstOrDefault();
        }

        public List<Car> GetCarsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCarsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCarsByYearRange(int startYear, int endYear)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCarsOrderedByPrice(bool ascending)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCarsWithCategories()
        {
            throw new NotImplementedException();
        }

        public List<HighlightedCar> GetHighlightedCars()
        {
            return ShopContext.HighlightedCars
                    .OrderBy(highlightedCar => highlightedCar.HighlightedCarId)
                    .ToList();
        }

        public List<Car> GetHomePageCars()
        {
            return ShopContext.Cars
                .OrderBy(car => car.CarId) // Araçları Id'ye göre sırala
                .ToList();
        }

        public List<Car> GetSearchResult(string searchString)
        {
            var cars = ShopContext.Cars
                .Where(i => EF.Functions.Like(i.Brand, $"%{searchString}%") || EF.Functions.Like(i.Model, $"%{searchString}%"))
                .ToList();

            return cars;
        }
    }
}