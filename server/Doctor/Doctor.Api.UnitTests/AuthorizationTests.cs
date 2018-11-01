using Doctor.Api.Models;
using Doctor.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Doctor.Api.UnitTests
{
    [TestClass]
    public class AuthorizationTests : BaseTest
    {
        [TestMethod]
        public async Task RegisterUser_UserIsValid_ReturnOk()
        {
            // Arrange
            var user = new CreateUserModel
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid() + "@test.com",
                Password = "aaa111!!!AAA"
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/account", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task RegisterUser_EmailIsAlreadyUsed_ReturnNotOk()
        {
            // Arrange
            var user = new CreateUserModel
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid() + "@test.com",
                Password = "aaa111!!!AAA"
            };

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            await Client.PostAsync("/api/account", content);

            // Act
            var response = await Client.PostAsync("/api/account", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task RegisterUser_UserIsSavedToDatabaseAsync()
        {
            // Arrange
            var user = new CreateUserModel
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid() + "@test.com",
                Password = "aaa111!!!AAA"
            };

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/account", content);

            // Assert
            using (var context = new AuthorizationDbContext(Options))
            {
                var dbUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                Assert.IsNotNull(dbUser);
                Assert.AreEqual(user.Email, dbUser.Email);
            }
        }

        [TestMethod]
        public async Task Login_UserIsValid_ReturnOk()
        {
            // Arrange
            var user = new CreateUserModel
            {
                UserName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid() + "@test.com",
                Password = "aaa111!!!AAA"
            };

            var login = new LoginModel
            {
                UserName = user.UserName,
                Password = user.Password
            };

            var registerContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var registerResponse = await Client.PostAsync("/api/account", registerContent);

            // Act
            var loginContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var loginResponse = await Client.PostAsync("/api/auth/login", loginContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
        }

        [TestMethod]
        public async Task Login_UserIsNotValid_ReturnUnauthorized()
        {
            // Arrange
            var login = new LoginModel
            {
                UserName = Guid.NewGuid().ToString(),
                Password = "aaa111!!!AAA"
            };

            // Act
            var loginContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var loginResponse = await Client.PostAsync("/api/auth/login", loginContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, loginResponse.StatusCode);
        }


    }
}
