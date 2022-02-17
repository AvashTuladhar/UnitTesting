﻿using Practise.Models;
using Practise.Repositories;

namespace Practise.Services
{
    public interface IUserServices
    {
        List<User> GetAllUsers();
        User? GetUserByID(int Id);
        void DeleteUser(User user);
        User CreateUser(User user);
    }
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User CreateUser(User user)
        {
           return userRepository.CreateUser(user); 
        }

        public void DeleteUser(User user)
        {
            userRepository.DeleteUser(user);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();    
        }

        public User? GetUserByID(int Id)
        {
            return userRepository.GetUserByID(Id);  
        }
    }
}
