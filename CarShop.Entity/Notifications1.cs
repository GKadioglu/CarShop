using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity
{
    public class Notifications1
    {
        public int NotificationsId { get; set; }
        public string Sender { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Bir bildirimin birden fazla kullanıcısı olabilir
        public List<NotificationUser1> NotificationsUsers { get; set; }
    }
}