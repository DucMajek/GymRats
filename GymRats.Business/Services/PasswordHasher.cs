using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

public interface IPasswordHasher
{
    public string HashPassword(String password);
    public bool VerifyPassword(string password, string storedHash);
}

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string saltHex = Convert.ToHexString(salt);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return $"{saltHex}:{hashed}";
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2) return false;

        string saltHex = parts[0];
        string storedHashedPassword = parts[1];


        byte[] salt = Convert.FromHexString(saltHex);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashed == storedHashedPassword;
    }
}