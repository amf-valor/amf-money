using System;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class Token
    {
        public const string Audience = "amfmoneyapi";
        public const string Issuer = "amfValor";
        public const int SecondsToExpiry = 1200;

        public string Hash { get; }
        public DateTime CreatedAt { get; }
        public DateTime ExpiryAt { get; }

        public Token(string hash, DateTime createdAt, DateTime expiryAt)
        {
            Hash = hash;
            CreatedAt = createdAt;
            ExpiryAt = expiryAt;
        }

        public Token() { }
    }
}
