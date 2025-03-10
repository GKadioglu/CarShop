using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity
{
    public class AdminMakeAnOfferUserMakeAnOffer
    {
        public int AdminOfferId { get; set; }
        public AdminMakeAnOffer AdminMakeAnOffer { get; set; }
        public int UserOfferId { get; set; }
        public UserMakeAnOffer UserMakeAnOffer { get; set; }
    }
}