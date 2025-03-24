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
            int newCarId = ShopContext.Cars
                                      .OrderByDescending(c => c.CarId)
                                      .FirstOrDefault()?.CarId + 1 ?? 1; // Eğer hiçbir araç yoksa 1'den başla

            
            var newCar = new Car
            {
                CarId = newCarId, 
                Brand = brand,
                Model = model,
                Price = price,
                Year = year
            };

            
            ShopContext.Cars.Add(newCar);
            ShopContext.SaveChanges();  

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

            ShopContext.SaveChanges(); 

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
                ShopContext.FavoriteCarCars.RemoveRange(carToDelete.FavoriteCarCars);

                ShopContext.CarCategories.RemoveRange(carToDelete.CarCategories);
                ShopContext.CarModels.RemoveRange(carToDelete.CarModels);

                ShopContext.Cars.Remove(carToDelete);

                ShopContext.SaveChanges();
            }

            return carToDelete;  
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
                .OrderBy(car => car.CarId) 
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