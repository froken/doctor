using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Database
{
    public class AuthorizationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthorizationDbContext()
        {
        }

        public AuthorizationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
