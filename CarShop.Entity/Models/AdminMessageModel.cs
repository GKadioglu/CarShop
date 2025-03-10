using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity.Models
{
    public class AdminMessageModel
    {
        public int AdminMessageId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public List<AnonimMessages> anonimMessages {get; set;}
    }

    public class AnonimMessages 
    {
         public int MessageId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Message { get; set; }
        public List<AnonimMessageCars> AnonimMessageCars {get; set;}
    }

     public class AnonimMessageCars 
    {
         public int CarId { get; set; }
        public string Brand { get; set; }  
        public string Model { get; set; } 
        public int Year { get; set; }    
        public decimal Price { get; set; } 
        public string? ImageUrl { get; set; }
    }
}