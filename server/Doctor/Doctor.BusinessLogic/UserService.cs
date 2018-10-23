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

            if (!_hashService.VerifyHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        
    }
}
