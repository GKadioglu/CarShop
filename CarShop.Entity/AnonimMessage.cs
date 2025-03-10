using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.obj;
using Core.Entities;

namespace CarShop.Entity
{
    public class AnonimMessage: IEntity
    {
        public int MessageId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Message { get; set; }
        public List<AnonimMessageCar> AnonimMessageCars { get; set; }
        public List<AdminMessageAnonimMessage> AdminMessageAnonimMessages {get; set;}

    }
}