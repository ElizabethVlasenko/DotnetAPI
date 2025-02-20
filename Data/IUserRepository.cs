﻿using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IUserRepository
    {
        //calls; not methods
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToRemove);
        public IEnumerable<User> GetUsers();
        public User GetSingleUsers(int userId);
        public UserSalary GetSingleUserSalary(int userId);
        public UserJobInfo GetSingleUserJobInfo(int userId);
    }
}
