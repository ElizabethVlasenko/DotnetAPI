using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DataContextDapper _dapper;
        public UserController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("TestConnection")]
        public DateTime TestConnection()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }

        //[HttpGet("GetUsers/{testValue}")]
        ////public IActionResult Test()
        //public string[] GetUsers(string testValue)
        //{
        //    string[] responceArray = new string[] { "test1", "test2", testValue };
        //    return responceArray;
        //}

        [HttpGet("GetUsers")]
        //public IActionResult Test()
        public IEnumerable<User> GetUsers()
        {
            string sql = @"
                    SELECT  [UserId]
                    , [FirstName]
                    , [LastName]
                    , [Email]
                    , [Gender]
                    , [Active]
                    FROM  TutorialAppSchema.Users;";

            IEnumerable<User> users = _dapper.LoadData<User>(sql);

            return users;
        }

        [HttpGet("GetSingleUsers/{userId}")]
        //public IActionResult Test()
        public User GetSingleUsers(int userId)
        {
            string sql = @"
                    SELECT  [UserId]
                    , [FirstName]
                    , [LastName]
                    , [Email]
                    , [Gender]
                    , [Active]
                    FROM  TutorialAppSchema.Users
                    WHERE UserId = " + userId.ToString();

            User user = _dapper.LoadDataSingle<User>(sql);

            return user;
        }
    }
}

