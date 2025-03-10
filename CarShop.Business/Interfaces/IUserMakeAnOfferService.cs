using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.Models;
using Core.Utilities.Results;

namespace CarShop.Business.Abstract
{
    public interface IUserMakeAnOfferService
    {
        IDataResult<UserMakeAnOfferModel> NewOffer(string fullname, string email, string phone, int offer, int carId);
        IDataResult <List<UserMakeAnOfferModel>> GetUserOffer();
    }
}