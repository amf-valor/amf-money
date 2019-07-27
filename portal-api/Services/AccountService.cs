using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class AccountService : Service, IAccountService
    {
        public AccountService(AmfMoneyContext context) : base(context){}

        public bool CheckIfExists(string email)
        {
            return _context.Accounts.Where(u => u.Email.Equals(email)).Any();   
        }

        public int SignUp(Account account)
        {
            var passwordSalt = CryptoService.CreateSalt();
            var passwordHash = CryptoService.ComputeSHA256Hash(
                Encoding.UTF8.GetBytes(account.Password), passwordSalt);
            var pinSalt = CryptoService.CreateSalt();
            var pinHash = CryptoService.ComputeSHA256Hash(
                Encoding.UTF8.GetBytes(account.Pin), pinSalt);

            var accountEntity = new AccountEntity()
            {
                Birth = account.Birth,
                Email = account.Email,
                HashedPassword = passwordHash,
                PasswordSalt = passwordSalt,
                HashedPin =  pinHash,
                PinSalt = pinSalt
            };

            _context.Accounts.Add(accountEntity);
            _context.SaveChanges();
            return accountEntity.Id;
        }
        public bool Authenticate(Credential credential, out Token token)
        {
            AccountEntity account =_context.Accounts.Where(u => u.Email.Equals(credential.Email)).FirstOrDefault();
            token = null;

            if (account == null)
                return false;

            var hashedPassword = CryptoService.ComputeSHA256Hash(Encoding.UTF8.GetBytes(credential.Password), 
                account.PasswordSalt);

            bool isValid = hashedPassword.SequenceEqual(account.HashedPassword);

            if (isValid)
            {
                token = Create(account.Id);
            }

            return isValid;
        }

        private Token Create(int accountId)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(accountId.ToString(), "id"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.NameId, accountId.ToString())
                    });

            var handler = new JwtSecurityTokenHandler();
            DateTime now = DateTime.Now;
            DateTime expiryAt = now.Add(TimeSpan.FromSeconds(Token.SecondsToExpiry));

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = Token.Issuer,
                Audience = Token.Audience,
                SigningCredentials = CredentialsHandler.Credentials,
                Subject = identity,
                NotBefore = now,
                Expires = expiryAt
            });

            return new Token(handler.WriteToken(securityToken), now, expiryAt);
        }
    }
}
