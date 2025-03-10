using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface INotificationRepository : IRepository<Notifications1>
    {
        Notifications1 AddNewNotification(string sender, string title, string description, DateTime createdDate);
        List<Notifications1> GetNotifications(string userName);
        Task<NotificationUser1> UpdateNotificationReadStatusAsync(string userName, int notificationId, bool reads);

    }
}