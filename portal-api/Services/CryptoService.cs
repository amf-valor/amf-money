using System.Linq;
using System.Security.Cryptography;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public static class CryptoService
    {
        public static byte[] ComputeSHA256Hash(byte[] text, byte[] salt)
        {
            byte[] saltedValue = text.Concat(salt).ToArray();
            return new SHA256Managed().ComputeHash(saltedValue);
        }
        public static byte[] CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[32];
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}
