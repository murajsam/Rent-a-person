using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace BusinessLayer
{
    public class UserBusiness
    {
        private readonly UserRepository usersRepository;

        public UserBusiness()
        {
            this.usersRepository = new UserRepository();
        }

        public bool RegisterUser(User user)
        {
            return (this.usersRepository.RegisterUser(user));
        }

        public User LoginUser(string username, string password) 
        {
            List<User> users = new List<User>();
            users = usersRepository.GetAllUsers()
            .Where(user => user.UserDetails.Username == username && user.UserDetails.Password == password).ToList();
            return users.Count > 0 ? users[0] : null;
        }

        public bool UpdateUser(User user) 
        { 
            return this.usersRepository.UpdateUser(user);
        }
    }
}
