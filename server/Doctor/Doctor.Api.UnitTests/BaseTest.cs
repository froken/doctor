using Doctor.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Doctor.Api.UnitTests
{
    [TestClass]
    public class BaseTest
    {
        public static DbContextOptions Options { get; set; }
        public static SqliteConnection Connection { get; set; }
        public static TestServer Server { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            Options = new DbContextOptionsBuilder<AuthorizationDbContext>()
                    .UseSqlite(Connection)
                    .Options;

            TestStartup.OptionsBuilder = (DbContextOptionsBuilder options) =>
            {
                options.UseSqlite(Connection);
            };

            var webHostBuilder = WebHost.CreateDefaultBuilder(new string[] { })
                .UseStartup<TestStartup>()
                .UseContentRoot(Path.GetFullPath("../../../../Doctor.Api"));
            
            Server = new TestServer(webHostBuilder);
        }

        public void CreateAuthorizationSchema()
        {
            using (var context = new AuthorizationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }
             
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Connection.Close();
            Server.Dispose();
        }
    }
}
