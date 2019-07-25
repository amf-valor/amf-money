using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public static class CredentialsHandler
    {
        public static SigningCredentials Credentials { get; }

        static CredentialsHandler()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                var key = new RsaSecurityKey(provider.ExportParameters(true));
                Credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            }
        }   
    }
}
