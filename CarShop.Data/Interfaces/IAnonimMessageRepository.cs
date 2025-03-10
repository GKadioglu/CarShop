using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using Core.DataAccess;

namespace CarShop.Data.Abstract
{
    public interface IAnonimMessageRepository : IRepository<AnonimMessage>
    {
        AnonimMessage NewMessage(string fullname, string email, string phone, string message, int carId);
        List<AnonimMessage> GetMessage();
        Task <AnonimMessage> RemoveUserMessage(string userName, int messageId);
        Task<AnonimMessage> GetByIdAsync(int id);
    }
}