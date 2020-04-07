using System;
using eLibrary.Models;

namespace eLibrary.Services.UserService
{
    public class UserService : IUserService
    {
        public User Authenticate(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException();

            var user = new User
            {
                Username = name,
                Superior = name == "admin"
            };

            return user;
        }
    }
}