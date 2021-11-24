using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Hasher
    {

        private static string GenerateHashString(HashAlgorithm algo, string text)
        {
            // Compute hash from text parameter
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Get has value in array of bytes
            var result = algo.Hash;

            // Return as hexadecimal string
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }
        public string MD5(string text)
        {
            var result = default(string);

            using (var algo = new MD5CryptoServiceProvider())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }


        /*public string Encrypt(string decrypted)
        {
            string hash = "Password@2021$";
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider triple = new TripleDESCryptoServiceProvider();

            triple.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            triple.Mode = CipherMode.ECB;

            ICryptoTransform transform = triple.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }


        public string Decrypt(string encrypted)
        {
            string hash = "Password@2021$";
            byte[] data = UTF8Encoding.UTF8.GetBytes(encrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider triple = new TripleDESCryptoServiceProvider();

            triple.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            triple.Mode = CipherMode.ECB;

            ICryptoTransform transform = triple.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);

        }*/

    }
}
