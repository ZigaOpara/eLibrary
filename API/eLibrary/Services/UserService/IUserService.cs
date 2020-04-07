using eLibrary.Models;

namespace eLibrary.Services.UserService
{
    public interface IUserService
    {
        User Authenticate(string name);
    }
}