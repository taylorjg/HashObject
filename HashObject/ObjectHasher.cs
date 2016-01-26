using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace HashObject
{
    public static class ObjectHasher
    {
        private static readonly NullStream NullBaseStream = new NullStream();

        public static string ComputeHash<T>(T obj)
        {
            using (var md5 = MD5.Create())
            {
                var jsonString = JsonConvert.SerializeObject(obj);
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                var hashBytes = md5.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string ComputeHashUsingStream<T>(T obj)
        {
            using (var md5 = MD5.Create())
            {
                using (var cryptoStream = new CryptoStream(NullBaseStream, md5, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                    {
                        var jsonSerializer = new JsonSerializer();
                        jsonSerializer.Serialize(jsonTextWriter, obj);
                    }
                }
                var hashBytes = md5.Hash;
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
