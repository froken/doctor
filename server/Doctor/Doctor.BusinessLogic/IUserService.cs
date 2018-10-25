using Doctor.Entities;

namespace Doctor.BusinessLogic
{
    public interface IUserService
    {
        User Authenticate(string login, string password);

        User CreateUser(User user);
    }
}
