using System;
using System.Security.Cryptography;
using System.Linq;

namespace SLouple.MVC.Account
{
    public class Hash
    {
        public static string GenerateSalt(int saltSize)
        {
            string saltChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string result = new string(Enumerable.Repeat(saltChars, saltSize).Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }

        public static bool DoesMatch(string inputString, string hashedString, string salt, HashAlgorithm algorithm)
        {
            if (hashedString == HashString(inputString, salt, algorithm))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string HashString(string inputString, string salt, HashAlgorithm algorithm)
        {
            if (inputString == null)
            {
                throw new ArgumentException("inputString cannot be null.");
            }
            if (algorithm == null)
            {
                throw new ArgumentException("algorithm cannot be null.");
            }

            if (salt == null)
            {
                throw new ArgumentException("saltString cannot be null.");
            }

            //Mix password with salt.
            byte[] stringWithSalt = System.Text.Encoding.UTF8.GetBytes(inputString + salt);

            //Compute hash.
            byte[] hashedBytes = algorithm.ComputeHash(stringWithSalt);

            // Convert hashed password bytes to string and remove "-".
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", string.Empty);

            // Return hashed password string in lowercase.
            return hashedString.ToLower();
        }
    }
}