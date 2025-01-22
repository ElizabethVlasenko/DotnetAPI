using AutoMapper;
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
        IUserRepository _userRepository;
        IMapper _mapper;
        public UserEFController(IConfiguration config, IUserRepository userRepository)
        {
            _entityFramework = new DataContextEF(config);

            _userRepository = userRepository;

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>().ReverseMap();
            }));
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
                //_mapper.Map(user, userDb);

                //if more than zero rows affected - return ok
                if (_userRepository.SaveChanges())
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

            User userDb = _mapper.Map<User>(user);

            _userRepository.AddEntity<User>(userDb);

            if (_userRepository.SaveChanges())
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
                _userRepository.RemoveEntity<User>(userDb);

                if (_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete User");
            }

            throw new Exception("Failed to Delete User");
        }

        //USER SALARY

        [HttpGet("GetSingleUserSalary/{userId}")]
        public UserSalary GetSingleUserSalary(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();

            if (userSalary != null)
            {
                return userSalary;
            }

            throw new Exception("Failed to Get User Salary");
        }

        [HttpPut("EditUserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? userSalaryDb = _entityFramework.UserSalary
                .Where(u => u.UserId == userSalary.UserId)
                .FirstOrDefault<UserSalary>();

            if (userSalaryDb != null)
            {
                //userSalaryDb.Salary = userSalary.Salary;
                _mapper.Map(userSalary, userSalaryDb);
                // _mapper.Map(record with new data, record that needs to be udpated)

                if (_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Edit User Salary");
            }
            throw new Exception("Failed to Edit User Salary");
        }

        [HttpPost("AddUserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {

            _userRepository.AddEntity<UserSalary>(userSalary);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Add User Salary");
        }

        [HttpDelete("DeleteUserSalary/{userId}")]
        public IActionResult DeleteUserSalary(int userId)
        {
            UserSalary? userSalaryDb = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();

            if (userSalaryDb != null)
            {
                _userRepository.RemoveEntity<UserSalary>(userSalaryDb);
                if (_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete User Salary");
            }

            throw new Exception("Failed to Delete User Salary");
        }

        //USER JOB INFO

        [HttpGet("GetSingleUserJobInfo/{userId}")]
        public UserJobInfo GetSingleUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }

            throw new Exception("Failed to Get User Job Info");
        }

        [HttpPut("EditUserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userJobInfo.UserId)
                .FirstOrDefault<UserJobInfo>();

            if (userJobInfoDb != null)
            {
                _mapper.Map(userJobInfo, userJobInfoDb);

                if (_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Edit User Job Info");
            }
            throw new Exception("Failed to Edit User Job Info");
        }

        [HttpPost("AddUserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            _userRepository.AddEntity<UserJobInfo>(userJobInfo);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Add User Job Info");
        }

        [HttpDelete("DeleteUserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();

            if (userJobInfoDb != null)
            {
                _userRepository.RemoveEntity<UserJobInfo>(userJobInfoDb);

                if (_userRepository.SaveChanges())
                {
                    return Ok();
                }
                throw new Exception("Failed to Delete User Job Info");
            }
            throw new Exception("Failed to Delete User Job Info");
        }
    }
}

