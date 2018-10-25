using Doctor.Entities;

namespace Doctor.BusinessLogic
{
    public class HashService : IHashService
    {
        public bool VerifyPasswordHash(string value, PasswordHash hash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(hash.Salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash.Password[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public PasswordHash CreatePasswordHash(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return new PasswordHash
                {
                    Password = hmac.Key,
                    Salt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                };
            }
        }
    }
}
