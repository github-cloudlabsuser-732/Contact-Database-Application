using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList, result.Model);
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Details(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(User));
            var user = (User)result.Model;
            Assert.AreEqual(userId, user.Id);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Details(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>();
            UserController.userlist = userList;
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Edit(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(User));
            var user = (User)result.Model;
            Assert.AreEqual(userId, user.Id);
        }

        [TestMethod]
        public void Edit_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Edit(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Edit_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;
            var user = new User { Id = 1, Name = "Updated John", Email = "updatedjohn@example.com" };

            // Act
            var result = controller.Edit(userId, user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            var updatedUser = userList.FirstOrDefault(u => u.Id == userId);
            Assert.AreEqual(user.Name, updatedUser.Name);
            Assert.AreEqual(user.Email, updatedUser.Email);
        }

        [TestMethod]
        public void Edit_InvalidUser_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;
            var user = new User { Id = 1, Name = "", Email = "updatedjohn@example.com" };

            // Act
            var result = controller.Edit(userId, user) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Delete_ExistingUserId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Delete(userId, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(userList.FirstOrDefault(u => u.Id == userId));
        }

        [TestMethod]
        public void Delete_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Delete(userId, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
    }
}
