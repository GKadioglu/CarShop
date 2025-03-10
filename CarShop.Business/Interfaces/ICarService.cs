using CarShop.Entity.Models;
using CarShop.Entity;
using Core.Utilities.Results;

namespace CarShop.Business.Abstract
{
    public interface ICarService
    {
        AddCarModel AddNewCar(string brand, string model, decimal price, int year, int categoryId, int modelId);
        IResult DeleteCar(int carId);
        IDataResult<CarDetailModel> GetCarByName(string carName);
        IDataResult<List<Car>> GetSearchResult(string searchString);
        IDataResult<Car> GetCarDetails(int id);
        IDataResult<CarDetailModel> GetCarByIdDetails(int id);
        IDataResult<List<HighlightedCar>> GetHighlightedCars();
        IDataResult<List<Car>> GetCarsByCategoryId(int categoryId);
        IDataResult<List<Car>> GetCarsOrderedByPrice(bool ascending);
        IDataResult<List<Car>> GetCarsByPriceRange(decimal minPrice, decimal maxPrice);
        IDataResult<List<Car>> GetCarsWithCategories();
        IDataResult<List<Car>> GetCarsByYearRange(int startYear, int endYear);
        IDataResult<List<Car>> GetHomePageCars();

        IResult Update(Car car, int[] modelIds);
        Task<IResult> CreateAsync(Car car);
        Task<IResult> UpdateAsync(Car carToUpdate, Car car);
        Task<IResult> DeleteAsync(Car car);
    }
}