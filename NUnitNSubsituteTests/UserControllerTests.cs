using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Practise.Controllers;
using Practise.Models;
using Practise.Services;
using System.Collections.Generic;

namespace PractiseTests
{
    [TestFixture]
    internal class UserControllerTests
    {
        private List<User> users = new List<User>();

        [SetUp]
        public void Setup()
        {
            users.Add(new User() { Id = 1, Name = "Avash" });
            users.Add(new User() { Id = 2, Name = "Nitesh" });
            users.Add(new User() { Id = 3, Name = "Avinash" });
        }

        [Test]
        public void GetAll_ReturnsListOfUsers()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            //Act
            userServiceMock.GetAllUsers().Returns(users);
            var result = userController.GetAll();
            //Assert
            Assert.IsInstanceOf(typeof(ActionResult<List<User>>),result);
            Assert.AreEqual(typeof(OkObjectResult), result.Result.GetType());
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetByID_ExistingUserId_ReturnsUser()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            //Act
            userServiceMock.GetUserByID(users[0].Id).Returns(users[0]);
            var result = userController.GetByID(1);
            //Assert
            Assert.IsInstanceOf(typeof(ActionResult<User>), result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());

        }

        [Test]
        public void GetByID_NonExistingUserId_ReturnsBadRequest()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            //Act
            userServiceMock.GetUserByID(4);
            var result = userController.GetByID(4);
            //Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Create_IfGivenInputsAreCorrect_ReturnsOk()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            User user = new User() { Id = 4, Name = "XYZ" };
            //Act
            userServiceMock.CreateUser(user).Returns(user);
            var result = userController.Create(user);
            //Assert
            Assert.IsInstanceOf(typeof(ActionResult<User>), result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Create_IfGivenInputAreIncorrect_ReturnBadRequest()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            userController.ModelState.AddModelError("key", "error message");
            User user = new User();
            //Act
            userServiceMock.CreateUser(user);
            var result = userController.Create(user);
            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void DeleteUser_CorrectId_ReturnsAccepted()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            //Act
            userServiceMock.GetUserByID(users[0].Id).Returns(users[0]);
            userServiceMock.DeleteUser(users[0]);
            var result = userController.DeleteUser(users[0].Id);
            //Asssert
            Assert.IsInstanceOf<AcceptedResult>(result.Result);
            Assert.That(result.Result, Is.TypeOf<AcceptedResult>());

        }

        [Test]
        public void DeleteUser_InCorrectId_ReturnsBadRequest()
        {
            //Arrange
            var userServiceMock = Substitute.For<IUserServices>();
            var userController = new UserController(userServiceMock);
            //Act
            userServiceMock.GetUserByID(users[0].Id);
            userServiceMock.DeleteUser(users[0]);
            var result = userController.DeleteUser(users[0].Id);
            //Asssert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }
    }
}
