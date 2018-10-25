using Dapper;
using Doctor.Entities;
using System.Data.SqlClient;
using System.Linq;

namespace Doctor.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string QueryUserQuery = "SELECT * FROM [dbo].[users] WITH (NOLOCK) WHERE [login] = @login";
        private const string CreateUserQuery = "INSERT INTO [dbo].[users] ([login], [email], [passwordHash], [passwordSalt]) " +
                                               "VALUES (@login, @email, @passwordHash, @passwordSalt); SELECT SCOPE_IDENTITY()";


        private readonly IDatabaseConfiguration _configuration;

        public UserRepository(IDatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User CreateUser(User user)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString()))
            {
                var parameters = new
                {
                    login = user.Login,
                    email = user.Email,
                    passwordHash = user.PasswordHash,
                    passwordSalt = user.PasswordSalt
                };

                var userId = connection.Query<int>(CreateUserQuery, parameters).Single();
                user.UserId = userId;
                return user;
            }
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
