using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Contants;
using CarShop.Data.Abstract;
using CarShop.Entity.Models;
using Core.Utilities.Results;

namespace CarShop.Business.Concrete
{
    public class AdminMakeAnOfferManager : IAdminMakeAnOfferService
    {
        private readonly IUnitOfWork _unitofwork;

        public AdminMakeAnOfferManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<IDataResult<List<AdminMakeAnOfferModel>>> GetAdminOffer()
        {
            var adminOffers = await _unitofwork.AdminMakeAnOffers.GetAdminOffer();

            if (adminOffers == null || !adminOffers.Any())
            {
                return new ErrorDataResult<List<AdminMakeAnOfferModel>>(OfferMessages.OfferNotFound);
            }

            var models = adminOffers.Select(adminOffer => new AdminMakeAnOfferModel
            {
                AdminOfferId = adminOffer.AdminOfferId,
                Sender = adminOffer.Sender,
                Receiver = adminOffer.Receiver,
                Acceptance = adminOffer.Acceptance,
                userMakeOffers = adminOffer.AdminMakeAnOfferUserMakeAnOffers
                    .Where(ama => ama.UserMakeAnOffer != null) 
                    .Select(ama => new UserMakeOffers
                    {
                        UserOfferId = ama.UserMakeAnOffer.UserOfferId,
                        FullName = ama.UserMakeAnOffer.FullName,
                        Email = ama.UserMakeAnOffer.Email,
                        PhoneNumber = ama.UserMakeAnOffer.PhoneNumber,
                        Offer = ama.UserMakeAnOffer.Offer,
                        UserMakeOfferCars = ama.UserMakeAnOffer.MakeAnOfferCars
                            .Where(c => c.Car != null) 
                            .Select(c => new UserMakeOfferCars
                            {
                                CarId = c.Car.CarId,
                                Brand = c.Car.Brand,
                                Model = c.Car.Model,
                                Year = c.Car.Year,
                                Price = c.Car.Price,
                                ImageUrl = c.Car.ImageUrl
                            }).ToList()
                    }).ToList()
            }).ToList();

            return new SuccessDataResult<List<AdminMakeAnOfferModel>>(models, OfferMessages.OfferFound);
        }


        public async Task<IDataResult<List<AdminMakeAnOfferModel>>> GetAdminOfferByEmail(string email)
        {
            var adminOffer = await _unitofwork.AdminMakeAnOffers.GetAdminOfferByEmail(email);

            if (adminOffer == null || !adminOffer.Any())
            {
                return new ErrorDataResult<List<AdminMakeAnOfferModel>>(OfferMessages.OfferNotFound);
            }

            var models = adminOffer.Select(adminOffer => new AdminMakeAnOfferModel
            {
                AdminOfferId = adminOffer.AdminOfferId,
                Sender = adminOffer.Sender,
                Receiver = adminOffer.Receiver,
                Acceptance = adminOffer.Acceptance,
                userMakeOffers = adminOffer.AdminMakeAnOfferUserMakeAnOffers
                    .Where(ama => ama.UserMakeAnOffer != null) 
                    .Select(ama => new UserMakeOffers
                    {
                        UserOfferId = ama.UserMakeAnOffer.UserOfferId,
                        FullName = ama.UserMakeAnOffer.FullName,
                        Email = ama.UserMakeAnOffer.Email,
                        PhoneNumber = ama.UserMakeAnOffer.PhoneNumber,
                        Offer = ama.UserMakeAnOffer.Offer,
                        UserMakeOfferCars = ama.UserMakeAnOffer.MakeAnOfferCars
                            .Where(c => c.Car != null) 
                            .Select(c => new UserMakeOfferCars
                            {
                                CarId = c.Car.CarId,
                                Brand = c.Car.Brand,
                                Model = c.Car.Model,
                                Year = c.Car.Year,
                                Price = c.Car.Price,
                                ImageUrl = c.Car.ImageUrl
                            }).ToList()
                    }).ToList()
            }).ToList();

            // Başarılı dönüş
            return new SuccessDataResult<List<AdminMakeAnOfferModel>>(models, OfferMessages.OfferFound);
        }

        public async Task<IDataResult<AdminMakeAnOfferModel>> ReplyOfferAsync(string sender, string receiver, bool acceptance, int userofferId)
        {
            var userOffer = await _unitofwork.UserMakeAnOffers.GetByIdAsync(userofferId);
            if (userOffer == null)
            {
                return new ErrorDataResult<AdminMakeAnOfferModel>(OfferMessages.OfferNotFound);
            }

            var adminOffer = await _unitofwork.AdminMakeAnOffers.ReplyOfferAsync(sender, receiver, acceptance, userofferId);
            if (adminOffer == null)
            {
                return new ErrorDataResult<AdminMakeAnOfferModel>(OfferMessages.ReplyNotOffer);
            }

            var adminReplyModel = new AdminMakeAnOfferModel
            {
                AdminOfferId = adminOffer.AdminOfferId,
                Sender = adminOffer.Sender,
                Receiver = adminOffer.Receiver,
                Acceptance = adminOffer.Acceptance,
                userMakeOffers = new List<UserMakeOffers>
                {
                    new UserMakeOffers
                    {
                      UserOfferId = userOffer.UserOfferId,
                      FullName = userOffer.FullName,
                      Email = userOffer.Email,
                      PhoneNumber = userOffer.PhoneNumber,
                      Offer = userOffer.Offer,
                      UserMakeOfferCars = userOffer.MakeAnOfferCars.Select(car => new UserMakeOfferCars
                      {
                         CarId = car.CarId,
                    Brand = car.Car.Brand,
                    Model = car.Car.Model,
                    Year = car.Car.Year,
                    Price = car.Car.Price,
                    ImageUrl = car.Car.ImageUrl

                      }).ToList()
                    }
                }
            };
            return new SuccessDataResult<AdminMakeAnOfferModel>(adminReplyModel, OfferMessages.ReplyOffer);
        }
    }
}