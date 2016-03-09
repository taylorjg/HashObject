using System;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

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
                    using (var bsonWriter = new BsonWriter(cryptoStream))
                    {
                        var jsonSerializer = new JsonSerializer();
                        jsonSerializer.Serialize(bsonWriter, obj);
                    }
                }
                var hashBytes = md5.Hash;
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
