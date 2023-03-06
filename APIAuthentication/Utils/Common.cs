using System.Security.Cryptography;

namespace APIAuthentication.Utils
{
    public class Common
    {
        /*
         * Function to create random salt string
         * 
         * 
         */
        public static byte[] GetRandomSalt(int length)
        {
            var random = RandomNumberGenerator.Create();
            byte[] salt = new byte[length];
            random.GetNonZeroBytes(salt);
            return salt;
        }

        /*
         * Function to create password with salt
         * 
         * 
         */
        public static byte[] SaltHashPassword(byte[] password, byte[] salt)
        {
            SHA256 algorithm = SHA256.Create();
            byte[] plainTextWithSaltBytes = new byte[password.Length + salt.Length];
            for (int i = 0; i < password.Length; i++)
            {
                plainTextWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[password.Length + i] = salt[i];
            }
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

    }
}
