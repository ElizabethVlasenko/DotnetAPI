using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserEFController : ControllerBase
    {
        DataContextEF _entityFramework;
        public UserEFController(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }


        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();

            return users;
        }

        [HttpGet("GetSingleUsers/{userId}")]
        public User GetSingleUsers(int userId)
        {
            User? user = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }

            throw new Exception("Failed to Get User");
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            //get user from the db
            User? userDb = _entityFramework.Users
                .Where(u => u.UserId == user.UserId)
                .FirstOrDefault<User>();

            if (userDb != null)
            {
                //edit values 
                userDb.Active = user.Active;
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Gender = user.Gender;

                //if more than zero rows affected - return ok
                if (_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }

                throw new Exception("Failed to Edit User");
            }

            throw new Exception("Failed to Edit User");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserDto user)
        {

            User userDb = new User();

            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;

            _entityFramework.Users.Add(userDb);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to Add User");
        }

        [HttpDelete("DeleteUser/{userId}")]

        public IActionResult DeleteUser(int userId)
        {
            User? userDb = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();

            if (userDb != null)
            {
                _entityFramework.Users.Remove(userDb);

                if (_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete User");
            }

            throw new Exception("Failed to Delete User");
        }
    }
}

