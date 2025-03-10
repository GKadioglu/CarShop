using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.Models;
using CarShop.Entity;
using Core.Utilities.Results;

namespace CarShop.Business.Abstract
{
    public interface IAnonimMessageService
    {
        IDataResult <AnonimMessageModel> NewMessage(string fullname, string email, string phone, string message, int carId);

        IDataResult <List<AnonimMessageModel>> GetMessage();
        IDataResult <AdminMessageModel> RemoveUserMessage(string userName, int messageId);


    }
}