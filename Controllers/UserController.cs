using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpGet("GetUsers/{testValue}")]
        //public IActionResult Test()
        public string[] GetUsers(string testValue)
        {
            string[] responceArray = new string[] { "test1", "test2", testValue };
            return responceArray;
        }
    }
}

