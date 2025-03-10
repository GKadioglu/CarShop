using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.API.Identity;
using CarShop.Entity.Models;
using Core.Utilities.Results;
using Microsoft.Identity.Client;

namespace CarShop.API.UserProcess.Abstract
{
    public interface IUserService
    {
        public Task<UserDetailModel> GetUserDetail(string userName);
        Task<UserDetailModel> GetUserByIdOrUsernameAsync(string name);
        public Task<List<AdminUserDetailModel>> GetAllUsers();
        public Task<User> GetUserByEmail(string email);
        public Task<User> DeleteUser(string userName);
        public Task<IDataResult<User>> UserEdit(string firstName, string lastName, string userName);


    }
}