using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Business.Contants;
using CarShop.Entity.Models;
using CarShop.API.Identity;
using CarShop.API.UserProcess.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarShop.API.UserProcess.Concrete
{
    public class UserManager : IUserService
    {
        private readonly UserManager<User> _userManager;  // User sınıfını kullanalım

        public UserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("E-posta adresi boş olamaz.", nameof(email));
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            return user;
        }

        public async Task<List<AdminUserDetailModel>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDetails = users.Select(user => new AdminUserDetailModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            }).ToList();

            return await Task.FromResult(userDetails);
        }

        public async Task<UserDetailModel> GetUserDetail(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            return new UserDetailModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
        }

        public async Task<User> DeleteUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Kullanıcı adı boş olamaz.", nameof(userName));
            }

            // Kullanıcıyı adıyla bul
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            // Kullanıcıyı sil
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Kullanıcı silinemedi.");
            }

            return user;  
        }

        public async Task<UserDetailModel> GetUserByIdOrUsernameAsync(string name)
        {
            var user = await _userManager.Users
                .Where(u => u.UserName == name)
                .Select(u => new UserDetailModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IDataResult<User>> UserEdit(string firstName, string lastName, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new ErrorDataResult<User>(UserMessages.UserNotFound);
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ErrorDataResult<User>(UserMessages.UserNotFound);
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            if (user.UserName != userName)
            {
                user.UserName = userName;
            }

            // Güncellemeyi kaydet
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new ErrorDataResult<User>(UserMessages.UserUpdateFailed);
            }

            return new SuccessDataResult<User>(user, UserMessages.UserUpdated);
        }


    }
}