using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Business.Contants;
using CarShop.Data.Abstract;
using CarShop.Entity.Models;
using CarShop.Entity;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;

namespace CarShop.Business.Concrete
{
    public class AdminMessageManager : IAdminMessageService
    {
        private readonly IUnitOfWork _unitofwork;


        public AdminMessageManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<IDataResult<List<AdminMessageModel>>> GetAdminMessagesByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Email boş geldi!");
                return new ErrorDataResult<List<AdminMessageModel>>("Geçerli bir email adresi giriniz.");
            }

            // 1. AdminMessage verisini veritabanından al
            Console.WriteLine($"GetAdminMessagesByEmail çağrıldı, email: {email}");

            var adminMessages = await _unitofwork.AdminMessages.GetAdminMessagesByEmail(email);

            if (adminMessages == null)
            {
                Console.WriteLine("AdminMessages null döndü.");
                return new ErrorDataResult<List<AdminMessageModel>>(AdminMessages.MessageNotFound);
            }
            if (!adminMessages.Any())
            {
                Console.WriteLine("AdminMessages boş bir liste döndü.");
                return new ErrorDataResult<List<AdminMessageModel>>(AdminMessages.MessageNotFound);
            }

            Console.WriteLine($"AdminMessages bulundu: {adminMessages.Count} adet mesaj.");

            // 2. AdminMessageModel listesini oluştur ve doldur
            var models = adminMessages.Select(adminMessage => new AdminMessageModel
            {
                AdminMessageId = adminMessage.AdminMessageId,
                Sender = adminMessage.Sender,
                Receiver = adminMessage.Receiver,
                Message = adminMessage.Message,
                anonimMessages = adminMessage.AdminMessageAnonimMessages
                    .Where(ama => ama.AnonimMessage != null)
                    .Select(ama => new AnonimMessages
                    {
                        MessageId = ama.AnonimMessage.MessageId,
                        FullName = ama.AnonimMessage.FullName,
                        Email = ama.AnonimMessage.Email,
                        PhoneNumber = ama.AnonimMessage.PhoneNumber,
                        Message = ama.AnonimMessage.Message,
                        AnonimMessageCars = ama.AnonimMessage.AnonimMessageCars
                            .Where(c => c.Car != null)
                            .Select(c => new AnonimMessageCars
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

            Console.WriteLine("Admin mesajları başarıyla dönüldü.");
            return new SuccessDataResult<List<AdminMessageModel>>(models, AdminMessages.MessageFound);
        }


        public async Task<IDataResult<AdminMessageModel>> AnswerMessageAsync(string sender, string receiver, string message, int messageId)
        {
            // Kullanıcı mesajını getir
            var anonimMessage = await _unitofwork.AnonimMessages.GetByIdAsync(messageId);
            if (anonimMessage == null)
            {
                return new ErrorDataResult<AdminMessageModel>(AdminMessages.MessageNotFound);
            }

            // Cevap mesajını kaydet
            var adminMessage = await _unitofwork.AdminMessages.AnswerMessageAsync(sender, receiver, message, messageId);
            if (adminMessage == null)
            {
                return new ErrorDataResult<AdminMessageModel>(AdminMessages.FailedSendMessage);
            }

            // Geriye dönecek modeli oluştur
            var adminMessageModel = new AdminMessageModel
            {
                AdminMessageId = adminMessage.AdminMessageId,
                Sender = adminMessage.Sender,
                Receiver = adminMessage.Receiver,
                Message = adminMessage.Message,
                anonimMessages = new List<AnonimMessages>
        {
            new AnonimMessages
            {
                MessageId = anonimMessage.MessageId,
                FullName = anonimMessage.FullName,
                Email = anonimMessage.Email,
                PhoneNumber = anonimMessage.PhoneNumber,
                Message = anonimMessage.Message,
                AnonimMessageCars = anonimMessage.AnonimMessageCars.Select(car => new AnonimMessageCars
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

            // Başarılı dönüş
            return new SuccessDataResult<AdminMessageModel>(adminMessageModel, AdminMessages.SendMessage);
        }


    }
}