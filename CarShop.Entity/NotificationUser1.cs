using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity
{
    public class NotificationUser1
    {
        public int Id { get; set; }
        public int NotificationsId { get; set; }
        public bool Reads { get; set; }
        public string UserName { get; set; }

        // Bir NotificationUser1 sadece bir Notification'a ait olabilir
        public Notifications1 Notification { get; set; }
    }
}