using System;
using System.Security.Cryptography;

namespace Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 256 bits
                return Convert.ToBase64String(hashBytes);
            }
        }

        public (string Hash, string Salt) GeneratePasswordHash(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[32]; // 256 bits
                rng.GetBytes(saltBytes);

                string salt = Convert.ToBase64String(saltBytes);
                string hash = HashPassword(password, salt);

                return (hash, salt);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            string newlyHashedPassword = HashPassword(password, salt);
            return hashedPassword == newlyHashedPassword;
        }
    }
}
