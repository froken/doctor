using Dapper;
using Doctor.Entities;
using System.Data.SqlClient;
using System.Linq;

namespace Doctor.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string QueryUserQuery = "SELECT * FROM [dbo].[users] WITH (NOLOCK) WHERE [login] = @login";
    
        private readonly IDatabaseConfiguration _configuration;

        public UserRepository(IDatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetUser(string login)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString()))
            {
                return connection.Query<User>(QueryUserQuery, new { login }).SingleOrDefault();
            }
        }
    }
}
