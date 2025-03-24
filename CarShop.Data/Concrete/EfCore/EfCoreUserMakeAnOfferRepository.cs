using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Data.Abstract;
using CarShop.Entity;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data.Concrete.EfCore
{
    public class EfCoreUserMakeAnOfferRepository : EfCoreGenericRepository<UserMakeAnOffer>, IUserMakeAnOfferRepository
    {
        public EfCoreUserMakeAnOfferRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public async Task<UserMakeAnOffer> GetByIdAsync(int id)
        {
            return await ShopContext.UserMakeAnOffers
               .Include(m => m.MakeAnOfferCars)
               .ThenInclude(m => m.Car)
               .FirstOrDefaultAsync(m => m.UserOfferId == id);
        }

        public List<UserMakeAnOffer> GetUserOffer()
        {
            return ShopContext.UserMakeAnOffers
                .Include(u => u.MakeAnOfferCars)
                    .ThenInclude(mc => mc.Car)
                .Include(u => u.AdminMakeAnOfferUserMakeAnOffers) 
                    .ThenInclude(a => a.AdminMakeAnOffer)
                .ToList();
        }

        public UserMakeAnOffer NewOffer(string fullname, string email, string phone, int offer, int carId)
        {
            var makeOffer = new UserMakeAnOffer
            {
                FullName = fullname,
                Email = email,
                PhoneNumber = phone,
                Offer = offer
            };
            ShopContext.UserMakeAnOffers.Add(makeOffer);
            ShopContext.SaveChanges();
            var makeOfferCar = new MakeAnOfferCar
            {
                CarId = carId,
                OfferId = makeOffer.UserOfferId
            };
            ShopContext.MakeAnOfferCars.Add(makeOfferCar);
            ShopContext.SaveChanges();
            return makeOffer;
        }
    }
}