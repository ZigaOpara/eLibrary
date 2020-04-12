using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var res = await _userService.GetUsers();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var res = await _userService.Authenticate(user.Username);
            return Ok(res);
        }
    }
}