using Doctor.Entities;

namespace Doctor.Repository
{
    public interface IUserRepository
    {
        User GetUser(string login);
    }
}
