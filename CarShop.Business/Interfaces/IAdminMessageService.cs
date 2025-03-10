using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity.Models;
using Core.Utilities.Results;

namespace CarShop.Business.Abstract
{
    public interface IAdminMessageService
    {
      Task <IDataResult<AdminMessageModel>> AnswerMessageAsync(string sender, string receiver, string message, int messageId);

      Task<IDataResult<List<AdminMessageModel>>> GetAdminMessagesByEmail (string email);

    }
}