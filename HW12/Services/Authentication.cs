
using HW12.Entities;
using HW12.Enums;
using HW12.Infrastructure;
using HW12.Interfaces.IRepository;
using HW12.LocalDb;
using HW12.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HW12.Services
{
    public class Authentication
    {
        private readonly IUserRepository _userrepo = new UserRepository();
        public bool Login(string username, string password)
        {
            var user = _userrepo.GetUserByUsername(username);

            if(user != null && user.Password == password)
            {
                LocalStorage.CurrentUser = user;
                return true;
            }
            throw new Exception("Username Or Password Is Incorrect");
        }
        public void Register(string username, string password, RoleEnum role)
        {
            var user = new User()
            {
                Username = username,
                Password = password,
                Role = role
            };
            _userrepo.AddUser(user);
        }
    }
}
