using Doctor.Entities;
using Doctor.Repository;

namespace Doctor.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IHashService _hashService;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public User CreateUser(User user)
        {
            var hash = _hashService.CreatePasswordHash(user.Password);

            user.PasswordHash = hash.Password;
            user.PasswordSalt = hash.Salt;

            user = _userRepository.CreateUser(user);

            return user;
        }

        public User Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _userRepository.GetUser(login);

            if (user == null)
            {
                return null;
            }

            if (!_hashService.VerifyPasswordHash(password, new PasswordHash { Password = user.PasswordHash, Salt = user.PasswordSalt }))
            {
                return null;
            }

            return user;
        }

        
    }
}
