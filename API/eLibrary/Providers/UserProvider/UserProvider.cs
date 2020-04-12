using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Repositories.UserRepository;

namespace eLibrary.Providers.UserProvider
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            var res = await _userRepository.GetUsers();
            return res;
        }

        public async Task<User> AddUser(User user)
        {
            var res = await _userRepository.AddUser(user);
            return res;
        }
    }
}