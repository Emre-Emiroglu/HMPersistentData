using System;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace CodeCatGames.HMPersistentData.Runtime
{
    public sealed class EncryptedJsonSerializer : ISerializer
    {
        #region ReadonlyFields
        private readonly byte[] _key;
        private readonly byte[] _iv;
        #endregion

        #region Constructor
        public EncryptedJsonSerializer(string base64Key, string base64Iv)
        {
            _key = Convert.FromBase64String(base64Key);
            _iv = Convert.FromBase64String(base64Iv);
        }
        #endregion
        
        #region Executes
        public string Serialize<T>(T obj) => Encrypt(JsonConvert.SerializeObject(obj));
        public T Deserialize<T>(string encryptedJson) => JsonConvert.DeserializeObject<T>(Decrypt(encryptedJson));
        private string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            
            aes.Key = _key;
            aes.IV = _iv;

            using MemoryStream ms = new();
            using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using StreamWriter sw = new(cs);
            
            sw.Write(plainText);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }
        private string Decrypt(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            
            aes.Key = _key;
            aes.IV = _iv;

            using MemoryStream ms = new(buffer);
            using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader sr = new(cs);

            return sr.ReadToEnd();
        }
        #endregion
    }
}