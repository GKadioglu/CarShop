using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.Models;
using Core.Utilities.Results;

namespace CarShop.Business.Abstract
{
    public interface IAdminMakeAnOfferService
    {
        Task<IDataResult<AdminMakeAnOfferModel>> ReplyOfferAsync(string sender, string receiver, bool acceptance, int userofferId);
        Task<IDataResult<List<AdminMakeAnOfferModel>>> GetAdminOfferByEmail(string email);
        Task<IDataResult<List<AdminMakeAnOfferModel>>> GetAdminOffer();

    }
}