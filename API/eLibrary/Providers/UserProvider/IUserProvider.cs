using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;

namespace eLibrary.Providers.UserProvider
{
    public interface IUserProvider
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> AddUser(User user);
    }
}