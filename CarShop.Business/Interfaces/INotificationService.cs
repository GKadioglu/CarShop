using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using CarShop.Entity.Models;
using Core.Utilities.Results;

namespace CarShop.Business.Abstract
{
    public interface INotificationService
    {
        AddNotificationModel AddNewNotification(string sender, string title, string description, DateTime createdDate);
        IDataResult<List<AddNotificationModel>> GetNotifications(string userName);
        Task<IDataResult<UserNotificationModel>> UpdateNotificationReadStatusAsync(string userName, int notificationId, bool reads);

    }
}