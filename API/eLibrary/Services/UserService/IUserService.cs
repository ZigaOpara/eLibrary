using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;

namespace eLibrary.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> Authenticate(string username);
    }
}