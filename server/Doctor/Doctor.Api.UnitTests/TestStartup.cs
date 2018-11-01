using Doctor.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Doctor.Api.UnitTests
{
    public class TestStartup : Startup
    {
        public static DbContextOptions Options { get; set; }

        public static Action<DbContextOptionsBuilder> OptionsBuilder { get; set; }

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void UpdateAuthorizationContextOptions(DbContextOptionsBuilder options)
        {
            OptionsBuilder(options);
            Options = options.Options;
        }
    }
}
