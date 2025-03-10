using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Data.Abstract;
using CarShop.Entity;
using CarShop.Entity.obj;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data.Concrete.EfCore
{
    public class EfCoreAnonimMessageRepository : EfCoreGenericRepository<AnonimMessage>, IAnonimMessageRepository
    {
        public EfCoreAnonimMessageRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public List<AnonimMessage> GetMessage()
        {
            return ShopContext.AnonimMessages
                              .Include(amc => amc.AnonimMessageCars)
                              .ThenInclude(amc => amc.Car)
                              .ToList();
        }

        public AnonimMessage NewMessage(string fullname, string email, string phone, string message, int carId)
        {
            var anonimMessage = new AnonimMessage
            {
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Message = message
            };
            ShopContext.AnonimMessages.Add(anonimMessage);
            ShopContext.SaveChanges(); // MessageId burada atanacak

            var anonimMessageCar = new AnonimMessageCar
            {
                CarId = carId,
                MessageId = anonimMessage.MessageId // Burada MessageId'yi güncelleyin
            };
            ShopContext.AnonimMessageCars.Add(anonimMessageCar);
            ShopContext.SaveChanges();

            return anonimMessage;

        }
        public async Task<AnonimMessage> GetByIdAsync(int id)
        {
            return await ShopContext.AnonimMessages
                .Include(m => m.AnonimMessageCars)
                .ThenInclude(m => m.Car)
                .FirstOrDefaultAsync(m => m.MessageId == id);
        }

        public async Task<AnonimMessage> RemoveUserMessage(string userName, int messageId)
        {
            var message = await ShopContext.AnonimMessages
                .Include(m => m.AnonimMessageCars)
                .Include(m => m.AdminMessageAnonimMessages)
                .ThenInclude(ama => ama.AdminMessage) // ✅ AdminMessage'ı da dahil ettik!
                .FirstOrDefaultAsync(m => m.MessageId == messageId);

            if (message == null)
            {
                return null; // Mesaj yoksa işlem yapma
            }

            ShopContext.AnonimMessageCars.RemoveRange(message.AnonimMessageCars);

            ShopContext.AdminMessageAnonimMessages.RemoveRange(message.AdminMessageAnonimMessages);

            foreach (var adminMessageAnonimMessage in message.AdminMessageAnonimMessages)
            {
                var adminMessage = await ShopContext.AdminMessages
                    .FirstOrDefaultAsync(am => am.AdminMessageId == adminMessageAnonimMessage.AdminMessageId);

                if (adminMessage != null)
                {
                    ShopContext.AdminMessages.Remove(adminMessage);
                }
            }

            ShopContext.AnonimMessages.Remove(message);

            await ShopContext.SaveChangesAsync();
            return message;
        }


    }
}