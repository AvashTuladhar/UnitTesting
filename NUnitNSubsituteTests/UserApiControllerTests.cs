using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Practise.Controllers;
using Practise.Models;
using Practise.Repositories;
using Practise.Services;
using System.Collections.Generic;

namespace PractiseTests
{
    [TestFixture]
    internal class UserApiControllerTests
    {
        private UserApiController userApiController;
        private IUserServices userServicesMock;
        private List<User> users = new List<User>();
        private User testUser = new User();
        [SetUp]
        public void Setup()
        {
            //Arrange
            users.Add(new User() { Id = 1, Name = "Avash" });
            users.Add(new User() { Id = 2, Name = "Nitesh" });
            users.Add(new User() { Id = 3, Name = "Avinash" });
            users.Add(new User() { Id = 4, Name = "Bhabuk" });

            testUser.Id = 1;
            testUser.Name = "Updated User";

            this.userServicesMock = Substitute.For<IUserServices>();
            this.userApiController = new UserApiController(userServicesMock);
        }

        [Test]
        public void Get_WhenCalled_ReturnsListOfUsers()
        {
            //Act
            userServicesMock.GetAllUsers().Returns(users);
            var result = userApiController.Get();
            var resultStatus = result as OkObjectResult;
            //Assert
            userServicesMock.Received(1).GetAllUsers();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 200);
            Assert.AreEqual(resultStatus?.Value, users);
        }

        [Test]
        public void GetById_CorrectUserID_ReturnsUser()
        {
            //Act 
            userServicesMock.GetUserByID(users[0].Id).Returns(users[0]);
            var result = userApiController.Get(users[0].Id);
            var resultStatus = result as OkObjectResult;
            //Assert
            userServicesMock.Received(1).GetUserByID(users[0].Id);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 200);
            Assert.AreEqual(resultStatus?.Value, users[0]);
        }

        [Test]
        public void GetById_InCorrectUserID_ReturnsNotFoundResult()
        {
            //Act
            userServicesMock.GetUserByID(0).ReturnsNull();
            var result = userApiController.Get(0);
            var resultStatus = result as NotFoundResult;
            //Assert
            userServicesMock.Received(1).GetUserByID(0);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 404);
        }

        [Test]
        public void Create_CorrectInput_ReturnsUser()
        {
            //Act 
            userServicesMock.CreateUser(testUser).Returns(testUser);
            var result = userApiController.Create(testUser);
            var resultStatus = result as OkObjectResult;
            //Assert
            userServicesMock.Received(1).CreateUser(testUser);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 200);
            Assert.AreEqual(resultStatus?.Value, testUser);
        }

        [Test]
        public void Create_InCorrectInput_BadRequestObjectResult()
        {
            //Act 
            userServicesMock.CreateUser(new User()).ReturnsNull();
            userApiController.ModelState.AddModelError("key", "error message");
            var result = userApiController.Create(new User());
            var resultStatus = result as BadRequestObjectResult;
            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 400);
        }

        [Test]
        public void Update_CorrectUser_ReturnsUser()
        {
            //Act
            userServicesMock.GetUserByID(testUser.Id).Returns(users[0]);
            userServicesMock.UpdateUser(testUser).Returns(testUser);
            var result = userApiController.Update(testUser.Id, testUser);
            var resultStatus = result as OkObjectResult;
            //Assert
            userServicesMock.Received().GetUserByID(testUser.Id);
            userServicesMock.Received().UpdateUser(testUser);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 200);
            Assert.AreEqual(resultStatus?.Value, testUser);
        }

        [Test]
        public void Update_IdMismatch_ReturnsBadRequest()
        {
            //Act
            userServicesMock.UpdateUser(testUser).ReturnsNull();
            var result = userApiController.Update(0, testUser);
            var resultStatus = result as BadRequestResult;
            //Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 400);
        }

        [Test]
        public void Update_IncompleteUser_ReturnsBadRequest()
        {
            //Arrange 
            User user = new User() { Id = 1, Name = null };
            //Act 
            userServicesMock.UpdateUser(user).ReturnsNull();
            userApiController.ModelState.AddModelError("key", "error message");
            var result = userApiController.Update(1, user);
            var resultStatus = result as BadRequestObjectResult;
            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 400);
        }

        [Test]
        public void Delete_CorrectID_ReturnsAccepted()
        {
            //Act
            userServicesMock.GetUserByID(testUser.Id).Returns(testUser);
            userServicesMock.DeleteUser(testUser);
            var result = userApiController.Delete(testUser.Id);
            var resultStatus = result as AcceptedResult;
            //Assert
            userServicesMock.Received().GetUserByID(testUser.Id);
            userServicesMock.Received().DeleteUser(testUser);
            Assert.That(result, Is.TypeOf<AcceptedResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 202);
        }

        [Test]
        public void Delete_InCorrectID_ReturnsNotFoundResult()
        {
            //Act
            userServicesMock.GetUserByID(0).ReturnsNull();
            var result = userApiController.Delete(0);
            var resultStatus = result as NotFoundResult;
            //Assert
            userServicesMock.Received().GetUserByID(0);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.IsNotNull(resultStatus);
            Assert.AreEqual(resultStatus?.StatusCode, 404);
        }
    }
}
