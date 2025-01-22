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
        //DataContextEF _entityFramework;
        IUserRepository _userRepository;
        IMapper _mapper;
        public UserEFController(IConfiguration config, IUserRepository userRepository)
        {
            //_entityFramework = new DataContextEF(config);

            _userRepository = userRepository;

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
            }));
        }


        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _userRepository.GetUsers();
            return users;
        }

        [HttpGet("GetSingleUsers/{userId}")]
        public User GetSingleUsers(int userId)
        {
            return _userRepository.GetSingleUsers(userId);
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            //get user from the db
            User? userDb = _userRepository.GetSingleUsers(user.UserId);

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
            User? userDb = _userRepository.GetSingleUsers(userId);

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
            return _userRepository.GetSingleUserSalary(userId);
        }

        [HttpPut("EditUserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? userSalaryDb = _userRepository.GetSingleUserSalary(userSalary.UserId);

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
            UserSalary? userSalaryDb = _userRepository.GetSingleUserSalary(userId);

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
            return _userRepository.GetSingleUserJobInfo(userId);
        }

        [HttpPut("EditUserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userJobInfo.UserId);

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
            UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userId);

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

