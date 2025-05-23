using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.obj;

namespace CarShop.Entity.Models
{
    public class AnonimMessageModel
    {
        public int MessageId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Message { get; set; }
        public List<AnonimMessageCar> AnonimMessageCars {get; set;}
    }

    public class AnonimMessageCar 
    {
         public int CarId { get; set; }
        public string Brand { get; set; }  
        public string Model { get; set; } 
        public int Year { get; set; }    
        public decimal Price { get; set; } 
        public string? ImageUrl { get; set; }
    }
}