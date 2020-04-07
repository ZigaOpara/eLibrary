using eLibrary.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Authenticate(string name)
        {
            var res =  _userService.Authenticate(name);
            return Ok(res);
        }
    }
}