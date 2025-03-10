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
    public class EfCoreAdminMakeAnOfferRepository : EfCoreGenericRepository<AdminMakeAnOffer>, IAdminMakeAnOfferRepository
    {
        public EfCoreAdminMakeAnOfferRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public async Task<List<AdminMakeAnOffer>> GetAdminOffer()
        {
            return await ShopContext.AdminMakeAnOffers
                .Include(ao => ao.AdminMakeAnOfferUserMakeAnOffers)
                .ThenInclude(aom => aom.UserMakeAnOffer)
                .ThenInclude(moc => moc.MakeAnOfferCars)
                .ThenInclude(maoc => maoc.Car)
                .ToListAsync();
        }

        public async Task<List<AdminMakeAnOffer>> GetAdminOfferByEmail(string email)
        {
            return await ShopContext.AdminMakeAnOffers
                    .Where(ao =>
                        ao.AdminMakeAnOfferUserMakeAnOffers
                        .Any(aom =>
                            aom.UserMakeAnOffer.Email == email))
                            .Include(ao => ao.AdminMakeAnOfferUserMakeAnOffers)
                            .ThenInclude(aom => aom.UserMakeAnOffer)
                            .ThenInclude(moc => moc.MakeAnOfferCars)
                            .ThenInclude(maoc => maoc.Car)
                            .ToListAsync();
        }

        public async Task<AdminMakeAnOffer> ReplyOfferAsync(string sender, string receiver, bool acceptance, int userofferId)
        {
            var adminOffer = new AdminMakeAnOffer
            {
                Sender = sender,
                Receiver = receiver,
                Acceptance = acceptance
            };
            await ShopContext.AdminMakeAnOffers.AddAsync(adminOffer);
            await ShopContext.SaveChangesAsync();

            var adminUserOffer = new AdminMakeAnOfferUserMakeAnOffer
            {
                UserOfferId = userofferId,
                AdminOfferId = adminOffer.AdminOfferId
            };
            await ShopContext.AdminMakeAnOfferUserMakeAnOffers.AddAsync(adminUserOffer);
            await ShopContext.SaveChangesAsync();

            return adminOffer;
        }
    }
}