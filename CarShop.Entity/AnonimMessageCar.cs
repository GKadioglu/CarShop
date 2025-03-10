using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity.obj
{
    public class AnonimMessageCar: IEntity
    {
        public int CarId { get; set; }       // Car tablosu ile ilişki (Foreign Key)
        public Car Car { get; set; }

        public int MessageId { get; set; }
        public AnonimMessage AnonimMessage {get; set;}
    }
}