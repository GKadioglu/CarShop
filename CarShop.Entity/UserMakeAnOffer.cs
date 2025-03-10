using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class UserMakeAnOffer : IEntity
    {
        public int UserOfferId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Offer { get; set; }
        public List<MakeAnOfferCar> MakeAnOfferCars { get; set; }
        public List<AdminMakeAnOfferUserMakeAnOffer> AdminMakeAnOfferUserMakeAnOffers { get; set; }
    }
}