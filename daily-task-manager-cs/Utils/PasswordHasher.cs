using System;
using System.Security.Cryptography;
using System.Text;

namespace daily_task_manager_cs.Utils
{
    /// <summary>
    /// Provides secure password hashing and verification using SHA256.
    /// Implements salt-based hashing for enhanced security.
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Size of the salt in bytes
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Size of the hash in bytes
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Number of iterations for the hash function
        /// </summary>
        private const int Iterations = 10000;

        /// <summary>
        /// Hashes a password with a randomly generated salt
        /// </summary>
        /// <param name="password">Plain text password to hash</param>
        /// <returns>Hashed password in format: salt:hash (base64 encoded)</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or empty.");
            }

            // Generate a random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create the hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            string base64Hash = Convert.ToBase64String(hashBytes);

            return base64Hash;
        }

        /// <summary>
        /// Verifies a password against a stored hash
        /// </summary>
        /// <param name="password">Plain text password to verify</param>
        /// <param name="hashedPassword">Stored hashed password</param>
        /// <returns>True if password matches, false otherwise</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            try
            {
                // Extract the bytes
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);

                // Get the salt
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // Compute the hash on the password the user entered
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Compare the results
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (FormatException)
            {
                // Invalid base64 string
                return false;
            }
            catch (Exception)
            {
                // Other errors
                return false;
            }
        }
    }
}