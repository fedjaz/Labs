using System;
using Converter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace FileManager
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

        public static string Encrypt(string JSON, byte[] key)
        {
            using(Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, new byte[key.Length]);
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

        public static string Decrypt(string str, byte[] key)
        {
            using(Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateDecryptor(key, new byte[key.Length]);
                using(MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(str)))
                {
                    using(CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader sr = new StreamReader(cryptoStream))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static void EncryptFile(string path, EncryptionOptions options, Logger logger)
        {
            if(options.EnableEncryption)
            {
                string encrypted;
                byte[] key;
                if(options.RandomKey)
                {
                    key = GenerateKey(16);
                    options.Key = key;
                }
                else
                {
                    key = options.Key;
                }

                try
                {
                    using(StreamReader sr = new StreamReader(path))
                    {
                        encrypted = Encrypt(sr.ReadToEnd(), key);
                    }
                    using(StreamWriter sw = new StreamWriter(path))
                    {
                        sw.Write(encrypted);
                    }
                    logger.Log("Encrypted file successfully");
                }
                catch
                {
                    logger.Log("Failed to encrypt file");
                }
            }
        }

        public static void DecryptFile(string path, EncryptionOptions options, Logger logger)
        {
            if(options.EnableEncryption) 
            { 
                try
                {
                    string decrypted;
                    byte[] key = options.Key;
                    using(StreamReader sr = new StreamReader(path))
                    {
                        decrypted = Decrypt(sr.ReadToEnd(), key);
                    }
                    using(StreamWriter sw = new StreamWriter(path))
                    {
                        sw.Write(decrypted);
                    }
                    logger.Log("Decrypted file successfully");
                }
                catch
                {
                    logger.Log("Failed to decrypt file");
                }
            }
        }
    }
}
