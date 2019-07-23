using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using System.Linq;
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
        public bool Authenticate(string email, string password)
        {
            AccountEntity account =_context.Accounts.Where(u => u.Email.Equals(email)).FirstOrDefault();

            if (account == null)
                return false;

            var hashedPassword = CryptoService.ComputeSHA256Hash(Encoding.UTF8.GetBytes(password), account.PasswordSalt);
            return hashedPassword.SequenceEqual(account.HashedPassword);
        }
    }
}
