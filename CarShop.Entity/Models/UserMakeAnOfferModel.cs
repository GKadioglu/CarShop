using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Entity.Models
{
    public class UserMakeAnOfferModel
    {
        public int UserOfferId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Offer { get; set; }
        public List<MakeAnOfferCars> MakeAnOfferCars { get; set; }

    }

    public class MakeAnOfferCars
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<AdminOffers> AdminOffers { get; set; }
    }
    public class AdminOffers
    {
        public int AdminOfferId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public bool Acceptance { get; set; }
    }

    public class NewOfferRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Offer { get; set; }
        public int CarId { get; set; }
    }
}