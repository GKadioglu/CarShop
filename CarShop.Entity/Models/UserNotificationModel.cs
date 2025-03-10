using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity.Models
{
    public class UserNotificationModel
    {
        public int NotificationsId { get; set; }
        public bool Reads { get; set; }
    }
}