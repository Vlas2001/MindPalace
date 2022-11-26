using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Service.Users;

internal class HashedPassword
{
    public string HashedPasswordText { get; set; }

    public string Salt { get; set; }
}

internal static class PasswordHasher
{
    internal static HashedPassword CalculateHash(string passwordText, byte[] salt) 
        => new()
            {
                HashedPasswordText = Convert.ToBase64String(CalculatedHashBytes(passwordText, salt)),
                Salt = Convert.ToBase64String(salt)
            };

    internal static bool PasswordIsCorrect(HashedPassword hashedPassword, string passwordText)
        => Convert.ToBase64String(CalculatedHashBytes(passwordText, Convert.FromBase64String(hashedPassword.Salt))) == hashedPassword.HashedPasswordText;

    private static byte[] CalculatedHashBytes(string passwordText, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(passwordText, salt, KeyDerivationPrf.HMACSHA256, 100000, 32);
    }
}