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
    public class EfCoreAdminMessageRepository : EfCoreGenericRepository<AdminMessage>, IAdminMessageRepository
    {
        public EfCoreAdminMessageRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public async Task<AdminMessage> AnswerMessageAsync(string sender, string receiver, string message, int messageId)
        {
            // Yeni admin mesajını oluştur
            var adminAnswer = new AdminMessage
            {
                Sender = sender,
                Receiver = receiver,
                Message = message
            };

            // Admin mesajını ekle
            await ShopContext.AdminMessages.AddAsync(adminAnswer);
            await ShopContext.SaveChangesAsync();

            // AdminMessageAnonimMessage ilişkisini kur
            var adminAnonimMessage = new AdminMessageAnonimMessage
            {
                MessageId = messageId,
                AdminMessageId = adminAnswer.AdminMessageId
            };

            await ShopContext.AdminMessageAnonimMessages.AddAsync(adminAnonimMessage);
            await ShopContext.SaveChangesAsync();

            return adminAnswer;
        }

        public async Task<List<AdminMessage>> GetAdminMessagesByEmail(string email)
        {
            return await ShopContext.AdminMessages
                .Where(amt =>
                    amt.AdminMessageAnonimMessages
                        .Any(amu =>
                            amu.AnonimMessage.Email == email))
                .Include(amy => amy.AdminMessageAnonimMessages)
                    .ThenInclude(amak => amak.AnonimMessage)
                        .ThenInclude(amv => amv.AnonimMessageCars)
                            .ThenInclude(amcv => amcv.Car)
                .ToListAsync();
        }


    }
}