using Microsoft.EntityFrameworkCore;

namespace Doctor.Api.Authorization
{
    public class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
