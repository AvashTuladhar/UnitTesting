using NSubstitute;
using NUnit.Framework;
using Practise.Models;
using Practise.Repositories;
using Practise.Services;
using System.Collections.Generic;

namespace PractiseTests
{
    [TestFixture]
    internal class UserServicesTests
    {

        [Test]
        public void CreateUser_WhenUserIsPassed_CreatesUserAndReturnUser()
        {
            //Arrange
            var userRepoMock = Substitute.For<IUserRepository>();
            var userServices = new UserServices(userRepoMock);
            User user = new User();
            user.Id = 100;
            user.Name = "Test";
            //Act
            userRepoMock.CreateUser(user).Returns(user);
            var result = userServices.CreateUser(user);
            //Assert
            Assert.AreEqual(user, result);
        }


        [Test]
        public void DeleteUser()
        {
            //Arrange
            var userRepo = Substitute.For<IUserRepository>();
            var userServices = new UserServices(userRepo);
            var user = new User() { Id = 1, Name = "Avash" };
            //Act
            userRepo.DeleteUser(user);
            //Assert
            userServices.DeleteUser(user);
        }

        [Test]
        public void GetAllUsers()
        {
            //Arrange
            var userRepoMock = Substitute.For<IUserRepository>();
            var userServices = new UserServices(userRepoMock);
            List<User> users = new List<User>();
            users.Add(new User() { Id = 1, Name = "Avash" });
            users.Add(new User() { Id = 2, Name = "Nitesh" });
            users.Add(new User() { Id = 3, Name = "Avinash" });
            //Act 
            userRepoMock.GetAllUsers().Returns(users);
            var result = userServices.GetAllUsers();
            //Assert
            Assert.AreEqual(result, users);
        }

        [Test]
        public void GetUserByID_CorrectId_ReturnsUser()
        {
            //Arrange
            var userRepoMock = Substitute.For<IUserRepository>();
            var userService = new UserServices(userRepoMock);
            List<User> users = new List<User>();
            users.Add(new User() { Id = 1, Name = "Avash" });
            users.Add(new User() { Id = 2, Name = "Nitesh" });
            users.Add(new User() { Id = 3, Name = "Avinash" });
            //Act
            userRepoMock.GetUserByID(1).Returns(users[0]);
            var result = userService.GetUserByID(1);
            //Assert
            Assert.AreEqual(result, users[0]);
        }

        [Test]
        public void GetUserByID_InCorrectId_ReturnsNull()
        {
            //Arrange
            var userRepoMock = Substitute.For<IUserRepository>();
            var userService = new UserServices(userRepoMock);
            List<User> users = new List<User>();
            users.Add(new User() { Id = 1, Name = "Avash" });
            users.Add(new User() { Id = 2, Name = "Nitesh" });
            users.Add(new User() { Id = 3, Name = "Avinash" });
            //Act
            userRepoMock.GetUserByID(4);
            var result = userService.GetUserByID(4);
            //Assert
            Assert.AreEqual(result, null);
        }
    }
}