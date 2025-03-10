using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace CarShop.Entity
{
    public class Message: IEntity
    {
        public int Id { get; set; }

        // Foreign Key for User (Sender)
        public string SenderId { get; set; }
        public virtual IdentityUser Sender { get; set; }  

        // Foreign Key for Admin/User (Receiver)
        public string ReceiverId { get; set; }
        public virtual IdentityUser Receiver { get; set; }  

        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public int CarId { get; set; }  
    }
}