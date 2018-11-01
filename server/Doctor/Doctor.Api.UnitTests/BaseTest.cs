using Doctor.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Http;

namespace Doctor.Api.UnitTests
{
    [TestClass]
    public class BaseTest
    {
        public static DbContextOptions Options { get; set; }
        public static SqliteConnection Connection { get; set; }
        public static TestServer Server { get; set; }
        public static HttpClient Client { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            
            TestStartup.OptionsBuilder = (DbContextOptionsBuilder options) =>
            {
                options.UseSqlite(Connection);
            };

            Startup.AuthorizationDbContextBuilder = (DbContextOptionsBuilder options, IConfiguration configuration) =>
            {
                options.UseSqlite(Connection);
                Options = options.Options;
            };

            var webHostBuilder = WebHost.CreateDefaultBuilder(new string[] { })
                .UseStartup<Startup>()
                .UseContentRoot(Path.GetFullPath("../../../../Doctor.Api"));
     
            Server = new TestServer(webHostBuilder);
            Server.Host.Services.GetService(typeof(AuthorizationDbContext));
            Client = Server.CreateClient();

            CreateAuthorizationSchema();
        }

        public static void CreateAuthorizationSchema()
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
            Client.Dispose();
            Server.Dispose();
        }
    }
}
