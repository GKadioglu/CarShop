using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity
{
    public class AdminMakeAnOffer
    {
        public int AdminOfferId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public bool Acceptance { get; set; }
        public List<AdminMakeAnOfferUserMakeAnOffer> AdminMakeAnOfferUserMakeAnOffers { get; set; }
    }
}