using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity.Models
{
    public class AddNotificationModel
    {
        public string Sender { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<AddNotificationUserModel> AddNotificationUserModels { get; set; }
    }

    public class AddNotificationUserModel
    {
        public int Id { get; set; }
        public int NotificationsId { get; set; }
        public bool Reads { get; set; }
        public string UserName { get; set; }
    }
}