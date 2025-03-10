using System;
using System.Collections.Generic;
using System.Linq;
using CarShop.Business.Abstract;
using CarShop.Data.Abstract;
using CarShop.Entity;
using CarShop.Entity.Models;
using CarShop.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Http;
using CarShop.Business.BusinessAspects.Autofac;

namespace CarShop.Business.Concrete
{
    public class FavoriteCarManager : IFavoriteCarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteCarManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddFavoriteCar(string userName, int carId)
        {
            await _unitOfWork.FavoriteCars.AddFavoriteCar(userName, carId);
        }

        public async Task<List<CarDetailModel>> GetFavoriteCars(string userName)
        {
            // Favori araçları al
            var favoriteCars = await _unitOfWork.FavoriteCars.GetFavoriteCars(userName);

            // Eğer favori araç yoksa boş bir liste döndür
            if (favoriteCars == null || !favoriteCars.Any())
            {
                return new List<CarDetailModel>();
            }

            // Favori araçları 'CarDetailModel' formatında döndür
            var carDetailModels = favoriteCars.Select(car => new CarDetailModel
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                ImageUrl = car.ImageUrl,
                Models = car.CarModels?.Select(model => new CarModelDetail
                {
                    ModelId = model.ModelId,
                    Name = model.Model?.Name, // Null kontrolü
                    Origin = model.Model?.Origin,
                    EstablishmentYear = model.Model.EstablishmentYear,
                    Founder = model.Model?.Founder
                }).ToList() ?? new List<CarModelDetail>(), // Null durumda boş liste
                Categories = car.CarCategories?.Select(category => new CarCategoryModelDetail
                {
                    CategoryId = category.CategoryId,
                    Name = category.Category?.Name // Null kontrolü
                }).ToList() ?? new List<CarCategoryModelDetail>(), // Null durumda boş liste
                CarFavoriteModels = car.FavoriteCarCars?.Select(fc => new CarFavoriteModel
                {
                    FavoriteId = fc.FavoriteId, // Burada FavoriteId'yi ilişki tablosundan alıyoruz
                    UserName = userName // Favori aracın UserName'ini ekliyoruz
                }).ToList() ?? new List<CarFavoriteModel>() // Null durumda boş liste
            }).ToList();

            return carDetailModels;
        }

        public async Task RemoveFavoriteCar(string userName, int carId)
        {
            await _unitOfWork.FavoriteCars.RemoveFavoriteCar(userName, carId);
        }
    }
}
