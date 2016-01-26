using System;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace HashObject
{
    public static class ObjectHasher
    {
        public static string ComputeHash<T>(T obj)
        {
            using (var md5 = MD5.Create())
            {
                using (var cryptoStream = new CryptoStream(Stream.Null, md5, CryptoStreamMode.Write))
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
