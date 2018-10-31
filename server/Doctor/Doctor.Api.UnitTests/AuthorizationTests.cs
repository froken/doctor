using Doctor.Api.Models;
using Doctor.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

//<PackageReference Include = "AspNet.Security.OAuth.Validation" Version="2.0.0-rc3-final" />
//    <PackageReference Include = "Automapper" Version="7.0.1" />
//    <PackageReference Include = "AutoMapper.Extensions.Microsoft.DependencyInjection" Version="5.0.1" />
//    <PackageReference Include = "Microsoft.AspNetCore.App" />
//    < PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.1.2" PrivateAssets="All" />
//    <PackageReference Include = "Microsoft.Data.Sqlite" Version="2.1.0" />
//    <PackageReference Include = "Microsoft.EntityFrameworkCore" Version="2.1.4" />
//    <PackageReference Include = "Microsoft.EntityFrameworkCore.Sqlite" Version="2.1.4" />
//    <PackageReference Include = "Microsoft.Extensions.Identity.Stores" Version="2.1.3" />
//    <PackageReference Include = "Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="2.1.1" />
//  </ItemGroup>

namespace Doctor.Api.UnitTests
{
    [TestClass]
    public class AuthorizationTests
    {
        [TestMethod]
        public async Task RegisterUser_UserIsSavedToDatabaseAsync()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Arrange
            var webHostBuilder = Program.CreateWebHostBuilder(Array.Empty<string>())
                .UseContentRoot(Path.GetFullPath("../../../../Doctor.Api"));

            var server = new TestServer(webHostBuilder);
            var client = server.CreateClient();

            try
            {
                var options = new DbContextOptionsBuilder<AuthorizationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new AuthorizationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new AuthorizationDbContext(options))
                {
                    var user = new CreateUserModel
                    {
                        UserName = "test_user",
                        Password = "aaa111!!!AAA"
                    };
                    // register
                    var content = new StringContent(user.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("/api/account", content);
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new AuthorizationDbContext(options))
                {
                    var userCount = await context.Users.CountAsync();
                    Assert.AreEqual(1, userCount);

                    var user = await context.Users.SingleAsync();
                    Assert.AreEqual("test_user", user.UserName);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
