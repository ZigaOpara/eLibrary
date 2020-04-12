using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Providers.UserProvider;

namespace eLibrary.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            var res = await _userProvider.GetUsers();
            return res;
        }

        public async Task<User> Authenticate(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException();

            var users = await _userProvider.GetUsers();
            var res = users.FirstOrDefault(user => user.Username == username);

            if (res != null) return res;
            
            return await _userProvider.AddUser(new User {Username = username});
        }
    }
}