using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Data.Abstract;
using CarShop.Entity.Models;
using CarShop.Entity;
using CarShop.Business.Contants;
using Core.Utilities.Results;


namespace CarShop.Business.Concrete
{
    public class AnonimMessageManager : IAnonimMessageService
    {
        private readonly IUnitOfWork _unitofwork;

        public AnonimMessageManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IDataResult<List<AnonimMessageModel>> GetMessage()
        {
            var anonimMessages = _unitofwork.AnonimMessages.GetMessage();

            if (anonimMessages == null || !anonimMessages.Any())
            {
                return new ErrorDataResult<List<AnonimMessageModel>>(AnonimContantsMessages.MessageNotFound);
            }

            var anonimMessageModels = new List<AnonimMessageModel>();

            foreach (var mesaj in anonimMessages)
            {
                var anonimMessageModel = new AnonimMessageModel
                {
                    MessageId = mesaj.MessageId,
                    FullName = mesaj.FullName,
                    Email = mesaj.Email,
                    PhoneNumber = mesaj.PhoneNumber,
                    Message = mesaj.Message,
                    AnonimMessageCars = mesaj.AnonimMessageCars
                        .Where(amc => amc.Car != null)  // Car null olmayanları filtrele
                        .Select(amc => new AnonimMessageCar
                        {
                            CarId = amc.CarId,
                            Brand = amc.Car.Brand,
                            Model = amc.Car.Model,
                            Year = amc.Car.Year,
                            Price = amc.Car.Price,
                            ImageUrl = amc.Car.ImageUrl
                        })
                        .ToList()
                };

                anonimMessageModels.Add(anonimMessageModel);
            }

            return new SuccessDataResult<List<AnonimMessageModel>>(anonimMessageModels, AnonimContantsMessages.MessageFound);
        }

        public IDataResult<AnonimMessageModel> NewMessage(string fullname, string email, string phone, string message, int carId)
        {
            var car = _unitofwork.Cars.GetCarDetails(carId);
            if (car == null)
            {
                return new ErrorDataResult<AnonimMessageModel>(CarMessages.CarNotFound);
            }

            var anonimMessage = new AnonimMessage
            {
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Message = message
            };

            var savedMessage = _unitofwork.AnonimMessages.NewMessage(fullname, email, phone, message, carId);
            if (savedMessage == null)
            {
                return new ErrorDataResult<AnonimMessageModel>(AnonimContantsMessages.FailedSendMessage); // Mesaj kaydedilemedi
            }

            var anonimMessageModel = new AnonimMessageModel
            {
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Message = message,
                AnonimMessageCars = new List<AnonimMessageCar>
        {
            new AnonimMessageCar
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

            return new SuccessDataResult<AnonimMessageModel>(anonimMessageModel, AnonimContantsMessages.SendMessage);
        }

        public IDataResult<AdminMessageModel> RemoveUserMessage(string userName, int messageId)
        {
            var message = _unitofwork.AnonimMessages.GetByIdAsync(messageId).Result;

            if (message == null)
            {
                return new ErrorDataResult<AdminMessageModel>(AnonimContantsMessages.MessageNotFound);
            }

            var deletedMessage = _unitofwork.AnonimMessages.RemoveUserMessage(userName, messageId).Result;


            var adminMessageModel = new AdminMessageModel
            {
                AdminMessageId = 0, // Bu mesaj admin mesajı olmadığı için 0 veya null bırakılabilir
                Sender = deletedMessage.FullName,
                Receiver = "admin", // Varsayılan olarak admin alıcı
                Message = deletedMessage.Message,
                anonimMessages = new List<AnonimMessages>
        {
            new AnonimMessages
            {
                MessageId = deletedMessage.MessageId,
                FullName = deletedMessage.FullName,
                Email = deletedMessage.Email,
                PhoneNumber = deletedMessage.PhoneNumber,
                Message = deletedMessage.Message,
                AnonimMessageCars = deletedMessage.AnonimMessageCars
                    .Select(amc => new AnonimMessageCars
                    {
                        CarId = amc.CarId,
                        Brand = amc.Car?.Brand,
                        Model = amc.Car?.Model,
                        Year = amc.Car?.Year ?? 0,
                        Price = amc.Car?.Price ?? 0,
                        ImageUrl = amc.Car?.ImageUrl
                    })
                    .ToList()
            }
        }
            };

            return new SuccessDataResult<AdminMessageModel>(adminMessageModel, AnonimContantsMessages.MessageDelete);
        }

    }
}