using Doctor.Entities;

namespace Doctor.BusinessLogic
{
    public interface IHashService
    {
        bool VerifyPasswordHash(string value, PasswordHash hash);

        PasswordHash CreatePasswordHash(string password);
    }
}
