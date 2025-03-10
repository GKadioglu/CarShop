using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface IUserMakeAnOfferRepository: IRepository<UserMakeAnOffer>
    {
        UserMakeAnOffer NewOffer(string fullname, string email, string phone, int offer, int carId);
        List<UserMakeAnOffer> GetUserOffer();
        Task<UserMakeAnOffer> GetByIdAsync(int id);
    }
}