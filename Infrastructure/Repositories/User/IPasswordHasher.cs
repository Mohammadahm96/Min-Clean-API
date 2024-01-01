namespace Infrastructure.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);
        (string Hash, string Salt) GeneratePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword, string salt);
    }
}

