using RsaAuth.Backend.Keys;
using System.Security.Cryptography;
using System.Text;
using CryptoRSA = System.Security.Cryptography.RSA;
namespace RsaAuth.Backend.Helpers
{
    public static class EncDscRSA
    {
        //private static readonly string publicKey;
        private static readonly string privateKey;
        static EncDscRSA()
        {
            //publicKey = RsaKeyManager.loadPublicKey();
            privateKey = RsaKeyManager.loadPrivateKey();
        }
       
        //public static string Encrypt(string data)
        //{

        //    using var rsa = CryptoRSA.Create();
        //    rsa.ImportFromPem(publicKey);

        //    var bytesToEncrypt = Encoding.UTF8.GetBytes(data);

        //    var encryptedBytes = rsa.Encrypt(bytesToEncrypt, RSAEncryptionPadding.Pkcs1);

        //    return Convert.ToBase64String(encryptedBytes);
        //}

        public static string Decrypt(string encrypted)
        {

            using var rsa = CryptoRSA.Create();
            rsa.ImportFromPem(privateKey);

            var encryptedBytes = Convert.FromBase64String(encrypted);

            var decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

    }
}

//EncDscRSA() runs only once because it’s static, and its fields(publicKey, privateKey) are static, so the constructor only executes when the class is first accessed.

//Is called automatically once before the class is first used(first access to any static member or method).

//readonly → Can only be assigned once, either inline or in the static constructor.Protects against accidental overwrites.
//static readonly = one copy, initialized once, immutable.