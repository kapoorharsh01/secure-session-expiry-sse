namespace RsaAuth.Backend.Keys
{
    public static class RsaKeyManager
    {
        //public static string loadPublicKey()
        //{
        //    return File.ReadAllText("Keys/public_key.pem");
        //}

        public static string loadPrivateKey() 
        {
            return File.ReadAllText("Keys/private_key.pem");
        }
    }
}
