using Doctor.Api.Models;
using Doctor.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Doctor.Api.UnitTests
{
    [TestClass]
    public class AuthorizationTests : BaseTest
    {
        [TestMethod]
        public async Task RegisterUser_UserIsSavedToDatabaseAsync()
        {
            // Arrange
            CreateAuthorizationSchema();
            var client = Server.CreateClient();
            var user = new CreateUserModel
            {
                UserName = "test_user",
                Email = "test@test.com",
                Password = "aaa111!!!AAA"
            };

            // Act
            using (var context = new AuthorizationDbContext(Options))
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/account", content);
            }

            // Assert
            using (var context = new AuthorizationDbContext(Options))
            {
                var userCount = await context.Users.CountAsync();
                Assert.AreEqual(1, userCount);

                var dbUser = await context.Users.SingleAsync();
                Assert.AreEqual("test_user", dbUser.UserName);
            }
        }
    }
}
