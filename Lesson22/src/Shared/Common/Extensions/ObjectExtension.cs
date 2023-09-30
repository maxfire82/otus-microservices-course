using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace Common.Extensions;

public static class ObjectExtension
{
    public static string GetMd5Hash(this object instance)
    {
        return instance.GetHash<MD5CryptoServiceProvider>();
    }
    
    public static string GetHash<T>(this object instance) where T : HashAlgorithm, new()
    {
        T cryptoServiceProvider = new T();
        return ComputeHash(instance, cryptoServiceProvider);
    }

    private static string ComputeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new()
    {
        DataContractSerializer serializer = new DataContractSerializer(instance.GetType());
        using (MemoryStream ms = new MemoryStream())
        {
            serializer.WriteObject(ms, instance);
            cryptoServiceProvider.ComputeHash(ms.ToArray());
            return Convert.ToHexString(cryptoServiceProvider.Hash);
        }
    }
}