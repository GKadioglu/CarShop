using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity
{
    public class MakeAnOfferCar
    {
        public int CarId { get; set; }       // Car tablosu ile ilişki (Foreign Key)
        public Car Car { get; set; }

        public int OfferId { get; set; }
        public UserMakeAnOffer MakeAnOffer { get; set; }
    }
}