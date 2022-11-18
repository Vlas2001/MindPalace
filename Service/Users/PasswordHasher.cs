using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Service.Users;

internal static class PasswordHasher
{
    internal static HashedPassword CalculateHash(string passwordText)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        return new HashedPassword
        {
            HashedPasswordText = Convert.ToBase64String(CalculatedHashBytes(passwordText, salt)),
            Salt = Convert.ToBase64String(salt)
        };
    }

    internal static bool PasswordIsCorrect(HashedPassword hashedPassword, string passwordText)
    {
        var salt = Convert.FromBase64String(hashedPassword.Salt);
        var hashedBytes = CalculatedHashBytes(passwordText, salt);

        return Convert.ToBase64String(hashedBytes) == hashedPassword.HashedPasswordText;
    }

    private static byte[] CalculatedHashBytes(string passwordText, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(password: passwordText!, salt: salt,
            prf: KeyDerivationPrf.HMACSHA256, iterationCount: 100_000, numBytesRequested: 256 / 8);
    }
}