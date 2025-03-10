using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Contants;
using CarShop.Data.Abstract;
using CarShop.Entity;
using CarShop.Entity.Models;
using Core.Utilities.Results;

namespace CarShop.Business.Concrete
{
    public class UserMakeAnOfferManager : IUserMakeAnOfferService
    {
        private readonly IUnitOfWork _unitofwork;

        public UserMakeAnOfferManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IDataResult<List<UserMakeAnOfferModel>> GetUserOffer()
        {
            var userMakeOffer = _unitofwork.UserMakeAnOffers.GetUserOffer();

            if (userMakeOffer == null || !userMakeOffer.Any())
            {
                return new ErrorDataResult<List<UserMakeAnOfferModel>>(OfferMessages.OfferNotFound);
            }

            var userMakeOfferModels = new List<UserMakeAnOfferModel>();

            foreach (var mesaj in userMakeOffer)
            {
                // Araba bilgilerini alıyoruz
                var userMakeOfferModel = new UserMakeAnOfferModel
                {
                    UserOfferId = mesaj.UserOfferId,
                    FullName = mesaj.FullName,
                    Email = mesaj.Email,
                    PhoneNumber = mesaj.PhoneNumber,
                    Offer = mesaj.Offer,
                    MakeAnOfferCars = mesaj.MakeAnOfferCars
                        .Where(amc => amc.Car != null)  // Car null olmayanları filtrele
                        .Select(amc => new MakeAnOfferCars
                        {
                            CarId = amc.CarId,
                            Brand = amc.Car.Brand,
                            Model = amc.Car.Model,
                            Year = amc.Car.Year,
                            Price = amc.Car.Price,
                            ImageUrl = amc.Car.ImageUrl,
                            AdminOffers = mesaj.AdminMakeAnOfferUserMakeAnOffers
                                .Where(a => a.AdminMakeAnOffer != null) // Null kontrolü
                                .Select(a => new AdminOffers
                                {
                                    AdminOfferId = a.AdminMakeAnOffer.AdminOfferId,
                                    Sender = a.AdminMakeAnOffer.Sender,
                                    Receiver = a.AdminMakeAnOffer.Receiver,
                                    Acceptance = a.AdminMakeAnOffer.Acceptance
                                })
                                .ToList()
                        })
                        .ToList()
                };

                userMakeOfferModels.Add(userMakeOfferModel);
            }

            // Başarılı dönüş
            return new SuccessDataResult<List<UserMakeAnOfferModel>>(userMakeOfferModels, OfferMessages.OfferFound);
        }


        public IDataResult<UserMakeAnOfferModel> NewOffer(string fullname, string email, string phone, int offer, int carId)
        {
            var car = _unitofwork.Cars.GetCarDetails(carId);
            if (car == null)
            {
                return new ErrorDataResult<UserMakeAnOfferModel>(CarMessages.CarNotFound);
            }
            var makeOffer = new UserMakeAnOffer
            {
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Offer = offer
            };
            var savedOffer = _unitofwork.UserMakeAnOffers.NewOffer(fullname, email, phone, offer, carId);
            if (savedOffer == null)
            {
                return new ErrorDataResult<UserMakeAnOfferModel>(OfferMessages.NotSaveOfferMade); // Teklif kaydedilemedi
            }

            // teklif modelini oluşturuyoruz
            var userMakeModel = new UserMakeAnOfferModel
            {
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Offer = offer,
                MakeAnOfferCars = new List<MakeAnOfferCars>
        {
            new MakeAnOfferCars
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                ImageUrl = car.ImageUrl
            }
        }
            };

            // Başarılı dönüş
            return new SuccessDataResult<UserMakeAnOfferModel>(userMakeModel, OfferMessages.OfferMade);
        }
    }
}