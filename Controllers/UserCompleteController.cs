using DotnetAPI.Data;
using DotnetAPI.Dtos;
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

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            string sql = @"
                UPDATE TutorialAppSchema.Users 
                SET
                 [FirstName] = '" + user.FirstName +
                "' , [LastName] = '" + user.LastName +
                "' , [Email] = '" + user.Email +
                "' , [Gender] = '" + user.Gender +
                "' , [Active] = '" + user.Active +
                "' WHERE userId = '" + user.UserId + "' ";


            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserDto user)
        {
            string sql = @"
                INSERT INTO  TutorialAppSchema.Users(
                  [FirstName]
                , [LastName]
                , [Email]
                , [Gender]
                , [Active]) VALUES ('" + user.FirstName +
                "' , '" + user.LastName +
                 "' , '" + user.Email +
                 "' , '" + user.Gender +
                 "' , '" + user.Active + "')";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Add User");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            string sql = @"
                DELETE FROM TutorialAppSchema.Users
                WHERE userId = '" + userId + "' ";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");
        }

        //USER SALARY

        [HttpGet("GetUserSalary/{userId}")]
        public UserSalary GetUserSalary(int userId)
        {
            string sql = @"
                SELECT [UserId]
                , [Salary]
                FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();

            UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);

            return userSalary;
        }

        [HttpPut("EditUserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            string sql = @"
                UPDATE TutorialAppSchema.UserSalary 
                SET
                 [Salary] = '" + userSalary.Salary +
                "' WHERE userId = '" + userSalary.UserId + "' ";


            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Salary");
        }


        [HttpPost("AddUserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            string sql = @"
                INSERT INTO  TutorialAppSchema.UserSalary(
                  [UserId]
                , [Salary]
                ) VALUES ('" + userSalary.UserId +
                "' , '" + userSalary.Salary + "')";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Add User Salary");
        }

        [HttpDelete("DeleteUserSalary/{userId}")]
        public IActionResult DeleteUserSalary(int userId)
        {
            string sql = @"
                DELETE FROM TutorialAppSchema.UserSalary
                WHERE userId = '" + userId + "' ";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Salary");
        }

        //USER JOB INFO

        [HttpGet("GetUserJobInfo/{userId}")]
        public UserJobInfo GetUserJobInfo(int userId)
        {
            string sql = @"
                SELECT [UserId]
                , [JobTitle]
                , [Department]
                FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();

            UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);

            return userJobInfo;
        }

        [HttpPut("EditUserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            string sql = @"
                UPDATE TutorialAppSchema.UserJobInfo 
                SET
                 [JobTitle] = '" + userJobInfo.JobTitle +
                "', [Department] = '" + userJobInfo.Department +
                "'  WHERE userId = '" + userJobInfo.UserId + "' ";


            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Job Info");
        }


        [HttpPost("AddUserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            string sql = @"
                INSERT INTO  TutorialAppSchema.UserJobInfo(
                  [UserId]
                , [JobTitle]
                , [Department]
                ) VALUES ('" + userJobInfo.UserId +
                "' , '" + userJobInfo.JobTitle +
                "' , '" + userJobInfo.Department + "')";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Add User Job Info");
        }

        [HttpDelete("DeleteUserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo(int userId)
        {
            string sql = @"
                DELETE FROM TutorialAppSchema.UserJobInfo
                WHERE userId = '" + userId + "' ";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Job Info");
        }
    }
}

