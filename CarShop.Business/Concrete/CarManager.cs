using CarShop.Business.Abstract;
using CarShop.Business.Contants;
using CarShop.Data.Abstract;
using CarShop.Entity.Models;
using CarShop.Entity;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Validation;
using CarShop.Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Aspects.Autofac.Exception;

namespace CarShop.Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly IUnitOfWork _unitofwork;

        public CarManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }


        public Task<IResult> CreateAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAsync(Car car)
        {
            throw new NotImplementedException();
        }

        [PerformanceAspect(interval: 5)]
        public IDataResult<List<HighlightedCar>> GetHighlightedCars()
        {
            var highlightedCars = _unitofwork.Cars.GetHighlightedCars();

            if (highlightedCars == null || !highlightedCars.Any())
            {
                return new ErrorDataResult<List<HighlightedCar>>(CarMessages.CarNotFound);
            }
            return new SuccessDataResult<List<HighlightedCar>>(highlightedCars, CarMessages.CarFound);
        }

        public IDataResult<List<Car>> GetCarsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Car>> GetCarsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Car>> GetCarsByYearRange(int startYear, int endYear)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Car>> GetCarsOrderedByPrice(bool ascending)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Car>> GetCarsWithCategories()
        {
            throw new NotImplementedException();
        }

        [PerformanceAspect(interval: 5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Car>> GetHomePageCars()
        {
            var cars = _unitofwork.Cars.GetHomePageCars();

            if (cars != null && cars.Any())
            {
                return new SuccessDataResult<List<Car>>(cars, CarMessages.CarFound);
            }

            return new ErrorDataResult<List<Car>>(CarMessages.CarNotFound);
        }

        public IResult Update(Car car, int[] modelIds)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(Car carToUpdate, Car car)
        {
            throw new NotImplementedException();
        }

        [PerformanceAspect(interval: 5)]
        public IDataResult<List<Car>> GetSearchResult(string searchString)
        {
            var cars = _unitofwork.Cars.GetSearchResult(searchString);

            if (cars != null && cars.Any())
            {
                return new SuccessDataResult<List<Car>>(cars, CarMessages.CarFound);
            }

            return new ErrorDataResult<List<Car>>(CarMessages.CarNotFound);
        }

        [PerformanceAspect(interval: 5)]
        public IDataResult<CarDetailModel> GetCarByName(string carName)
        {
            var carByName = _unitofwork.Cars.GetCarByName(carName);

            if (carByName == null)
            {
                return new ErrorDataResult<CarDetailModel>(CarMessages.CarNotFound);
            }

            var carViewModel = new CarDetailModel()
            {
                CarId = carByName.CarId,
                Brand = carByName.Brand,
                Model = carByName.Model,
                Year = carByName.Year,
                Price = carByName.Price,
                ImageUrl = carByName.ImageUrl,
                Models = carByName.CarModels.Select(cm => new CarModelDetail
                {
                    ModelId = cm.ModelId,
                    Name = cm.Model.Name,
                    Origin = cm.Model.Origin,
                    EstablishmentYear = cm.Model.EstablishmentYear,
                    Founder = cm.Model.Founder
                }).ToList(),
                Categories = carByName.CarCategories.Select(cc => new CarCategoryModelDetail
                {
                    CategoryId = cc.CategoryId,
                    Name = cc.Category.Name
                }).ToList()
            };

            return new SuccessDataResult<CarDetailModel>(carViewModel, CarMessages.CarFound);
        }

        [PerformanceAspect(interval: 5)]
        public IDataResult<Car> GetCarDetails(int id)
        {
            var car = _unitofwork.Cars.GetCarDetails(id);

            if (car != null)
            {
                return new SuccessDataResult<Car>(car, CarMessages.CarFound);
            }
            return new ErrorDataResult<Car>(CarMessages.CarNotFound);
        }

        [PerformanceAspect(interval: 5)]
        public IDataResult<CarDetailModel> GetCarByIdDetails(int id)
        {
            var carById = _unitofwork.Cars.GetCarDetails(id);
            if (carById != null)
            {
                var carDetailModel = new CarDetailModel
                {
                    CarId = carById.CarId,
                    Brand = carById.Brand,
                    Model = carById.Model,
                    Year = carById.Year,
                    Price = carById.Price,
                    ImageUrl = carById.ImageUrl,
                    Models = carById.CarModels.Select(cm => new CarModelDetail
                    {
                        ModelId = cm.ModelId,
                        Name = cm.Model.Name,
                        Origin = cm.Model.Origin,
                        EstablishmentYear = cm.Model.EstablishmentYear,
                        Founder = cm.Model.Founder
                    }).ToList()
                };
                return new SuccessDataResult<CarDetailModel>(carDetailModel, CarMessages.CarFound);
            }
            return new ErrorDataResult<CarDetailModel>(CarMessages.CarNotFound);
        }

        public IResult DeleteCar(int carId)
        {
            var deletebyId = _unitofwork.Cars.DeleteCar(carId);
            if (deletebyId != null)
            {
                return new SuccessResult(CarMessages.CarDeleted);
            }

            return new ErrorResult(false, CarMessages.CarAddFailed);
        }

        [ValidationAspect(typeof(CarValidator))]
        public AddCarModel AddNewCar(string brand, string model, decimal price, int year, int categoryId, int modelId)
        {

            var newCar = _unitofwork.Cars.AddNewCar(brand, model, price, year, categoryId, modelId);

            var addCarModel = new AddCarModel
            {
                Brand = newCar.Brand,
                Model = newCar.Model,
                Price = newCar.Price,
                Year = newCar.Year,
                CategoryId = newCar.CarCategories.FirstOrDefault()?.CategoryId ?? 0,
                ModelId = newCar.CarModels.FirstOrDefault()?.ModelId ?? 0
            };

            return addCarModel;
        }
    }
}
