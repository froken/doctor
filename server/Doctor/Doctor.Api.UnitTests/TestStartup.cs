using Doctor.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.Api.UnitTests
{
    public class TestStartup : Startup
    {
        public static SqliteConnection Connection { get; set; }

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<AuthorizationDbContext>(options => options.UseSqlite(Connection));
        }
    }
}
