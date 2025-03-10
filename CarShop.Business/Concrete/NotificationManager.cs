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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CarShop.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly IUnitOfWork _unitofwork;

        public NotificationManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public AddNotificationModel AddNewNotification(string sender, string title, string description, DateTime createdDate)
        {
            try
            {
                // Yeni bildirimi veritabanına ekliyoruz.
                var newNotification = _unitofwork.Notifications.AddNewNotification(sender, title, description, createdDate);

                // Model nesnesine dönüştürme
                var addNotificationModel = new AddNotificationModel
                {
                    Sender = newNotification.Sender,
                    Title = newNotification.Title,
                    Description = newNotification.Description,
                    CreatedDate = newNotification.CreatedDate
                };

                return addNotificationModel;
            }
            catch (Exception ex)
            {
                throw new Exception(AdminMessages.FailedSendNotification, ex);
            }
        }

        public IDataResult<List<AddNotificationModel>> GetNotifications(string userName)
        {
            try
            {
                var notifications = _unitofwork.Notifications.GetNotifications(userName); 

                var notificationModels = notifications.Select(n => new AddNotificationModel
                {
                    Sender = n.Sender,
                    Title = n.Title,
                    Description = n.Description,
                    CreatedDate = n.CreatedDate,
                    AddNotificationUserModels = n.NotificationsUsers
                        .Where(nu => nu.UserName == userName) // SADECE giriş yapan kullanıcı
                        .Select(nu => new AddNotificationUserModel
                        {
                            Id = nu.Id,
                            NotificationsId = nu.NotificationsId,
                            Reads = nu.Reads,
                            UserName = nu.UserName
                        }).ToList()
                }).ToList();

                return new SuccessDataResult<List<AddNotificationModel>>(notificationModels);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AddNotificationModel>>($"Error occurred: {ex.Message}");
            }
        }

        public async Task<IDataResult<UserNotificationModel>> UpdateNotificationReadStatusAsync(string userName, int notificationId, bool reads)
        {
            try
            {
                var result = await _unitofwork.Notifications.UpdateNotificationReadStatusAsync(userName, notificationId, reads);

                if (result == null)
                {
                    return new ErrorDataResult<UserNotificationModel>(null, "Notification update failed.");
                }

                // Eğer güncelleme başarılıysa, ilgili model ile dönüyoruz
                return new SuccessDataResult<UserNotificationModel>(
                    new UserNotificationModel
                    {
                        NotificationsId = notificationId,
                        Reads = reads
                    },
                    UserMessages.NotificationReadStatusUpdated
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UserNotificationModel>(
                    null,
                    UserMessages.NotificationReadStatusUpdateFailed
                );
            }
        }

    }
}
