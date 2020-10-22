using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Second
{
    static class Encryption
    {
        public static byte[] GenerateKey(int length)
        {
            byte[] arr = new byte[length];
            Random r = new Random();
            for(int i = 0; i < length; i++)
            {
                arr[i] = (byte)r.Next(0, 256);
            }
            return arr;
        }

        public static string Encrypt(object obj, byte[] key)
        {
            string JSON = JsonConvert.SerializeObject(obj);
            using(Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, new byte[16]);
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    using(CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cryptoStream))
                        {
                            sw.Write(JSON);
                        }

                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static T Decrypt<T>(string str, byte[] key)
        {
            string decryptedJSON;
            using(Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateDecryptor(key, new byte[16]);
                using(MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(str)))
                {
                    using(CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader sr = new StreamReader(cryptoStream))
                        {
                            decryptedJSON = sr.ReadToEnd();
                        }
                    }
                }
            }
            return JsonConvert.DeserializeObject<T>(decryptedJSON);
        }
    }
}
