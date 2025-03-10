using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface ICarRepository : IRepository<Car>
    {

        List<Car> GetSearchResult(string searchString);
        Car DeleteCar (int carId);
        Car AddNewCar (string brand, string model, decimal price, int year, int categoryId, int modelId);
        Car GetCarByName(string carName);
        Car GetCarDetails(int id);
        List<HighlightedCar> GetHighlightedCars();
        List<Car> GetCarsByCategoryId(int categoryId);
        List<Car> GetCarsOrderedByPrice(bool ascending);
        List<Car> GetCarsByPriceRange(decimal minPrice, decimal maxPrice);
        List<Car> GetCarsWithCategories();
        List<Car> GetHomePageCars();
        List<Car> GetCarsByYearRange(int startYear, int endYear);

    }
}