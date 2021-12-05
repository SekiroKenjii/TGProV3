using Core.Wrappers;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    public static class PasswordHelper
    {
        public static PasswordHandleResult HashPassword(this string password)
        {
            using var hmac = new HMACSHA512();
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return new PasswordHandleResult
            {
                PasswordHash = Convert.ToBase64String(computeHash),
                PasswordSalt = Convert.ToBase64String(hmac.Key)
            };
        }

        public static bool ValidatePassword(string enteredPassword, string savedPassword, string salt)
        {
            using var hmac = new HMACSHA512(Convert.FromBase64String(salt));

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

            var passwordHash = Convert.FromBase64String(savedPassword);

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != passwordHash[i]) return false;
            }

            return true;
        }
    }
}
