using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace CarShop.Data.Abstract
{
    public interface IAdminMessageRepository: IRepository<AdminMessage>
    {
         Task<AdminMessage> AnswerMessageAsync(string sender, string receiver, string message, int messageId);

         Task<List<AdminMessage>> GetAdminMessagesByEmail (string email );
    }
}