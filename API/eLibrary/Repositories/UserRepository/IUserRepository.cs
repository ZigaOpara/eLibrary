using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;

namespace eLibrary.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> AddUser(User user);
    }
}