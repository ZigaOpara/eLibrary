using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace eLibrary.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        
        public TestController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            var constr = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
            return Ok(constr);
        }
    }
}