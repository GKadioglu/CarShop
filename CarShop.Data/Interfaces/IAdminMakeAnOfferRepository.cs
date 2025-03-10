using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface IAdminMakeAnOfferRepository : IRepository<AdminMakeAnOffer>
    {
        Task<AdminMakeAnOffer> ReplyOfferAsync(string sender, string receiver, bool acceptance, int userofferId);
        Task<List<AdminMakeAnOffer>> GetAdminOffer ();
        Task<List<AdminMakeAnOffer>> GetAdminOfferByEmail (string email );
    }
}