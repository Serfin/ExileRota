using System;
using System.Security.Cryptography;

namespace ExileRota.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationsCount = 10000;
        private static readonly int SaltSize = 40;

        public string GetHash(string password, string salt)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Cannot generate hash from empty value");
            }
            if (String.IsNullOrEmpty(salt))
            {
                throw new Exception("Cannot use empty salt for hashing value");
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        public string GetSalt(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Cannot generate salt from an empty value");
            }

            byte[] saltBytes = new byte[SaltSize];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}