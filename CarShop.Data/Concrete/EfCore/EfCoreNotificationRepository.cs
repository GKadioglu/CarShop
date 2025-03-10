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
    public class EfCoreNotificationRepository : EfCoreGenericRepository<Notifications1>, INotificationRepository

    {
        public EfCoreNotificationRepository(ShopContext context) : base(context)
        {

        }

        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public Notifications1 AddNewNotification(string sender, string title, string description, DateTime createdDate)
        {
            // Yeni bir Notification nesnesi oluşturuyoruz
            var notification = new Notifications1
            {
                Sender = sender,
                Title = title,
                Description = description,
                CreatedDate = createdDate
            };

            // ShopContext üzerinden Notifications tablosuna yeni kayıt ekliyoruz
            ShopContext.Notifications1.Add(notification);

            // Veritabanına kaydetme işlemi
            ShopContext.SaveChanges();

            // Oluşturduğumuz notification'ı geri döndürüyoruz
            return notification;
        }

        public List<Notifications1> GetNotifications(string userName)
        {
            return ShopContext.Notifications1
                        .OrderByDescending(n => n.NotificationsId)
                        .Include(n => n.NotificationsUsers.Where(nu => nu.UserName == userName)) // SADECE giriş yapan kullanıcı
                        .ToList();
        }

        public async Task<NotificationUser1> UpdateNotificationReadStatusAsync(string userName, int notificationId, bool reads)
        {
            var notificationUser = await ShopContext.NotificationsUsers1
                .FirstOrDefaultAsync(nu => nu.UserName == userName && nu.NotificationsId == notificationId);

            if (notificationUser != null)
            {
                notificationUser.Reads = reads;
            }
            else
            {
                notificationUser = new NotificationUser1
                {
                    UserName = userName,
                    NotificationsId = notificationId,
                    Reads = reads
                };
                await ShopContext.NotificationsUsers1.AddAsync(notificationUser);
            }

            await ShopContext.SaveChangesAsync();
            return notificationUser;
        }
    }
}
