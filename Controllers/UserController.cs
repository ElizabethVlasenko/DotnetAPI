using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserCompleteController : ControllerBase
    {
        DataContextDapper _dapper;
        public UserCompleteController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("GetUsers/{userId}/{isActive}")]
        public IEnumerable<UserComplete> GetUsers(int userId = 0, bool isActive = false)
        {
            string sql = @"EXEC TutorialAppSchema.spUsers_Get";

            string parameters = "";

            if (userId != 0)
            {
                parameters += ", @UserId=" + userId.ToString();
            }

            if (isActive)
            {
                parameters += ", @Active=" + isActive.ToString();
            }

            if (parameters.Length > 0) sql += parameters.Substring(1);

            IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);

            return users;
        }

        [HttpPut("UpsertUser")]
        public IActionResult UpsertUser(UserComplete user)
        {
            string sql = @"EXEC TutorialAppSchema.spUser_Upsert 
                @FirstName = '" + user.FirstName +
                "', @LastName = '" + user.LastName +
                "', @Email = '" + user.Email +
                "', @Gender = '" + user.Gender +
                "', @Active = '" + user.Active +
                "', @JobTitle = '" + user.JobTitle +
                "', @Department = '" + user.Department +
                "', @salary = '" + user.Salary +
                "', @userId = '" + user.UserId + "' ";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            string sql = @"EXEC TutorialAppSchema.spUser_Delete
                @userId = '" + userId + "' ";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");
        }
    }
}

