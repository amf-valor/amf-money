using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using System.Text;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class UserService : Service, IUserService
    {
        public UserService(AmfMoneyContext context) : base(context){}
        public int SignUp(User user)
        {
            var passwordSalt = CryptoService.CreateSalt();
            var passwordHash = CryptoService.ComputeSHA256Hash(
                Encoding.UTF8.GetBytes(user.Password), passwordSalt);
            var pinSalt = CryptoService.CreateSalt();
            var pinHash = CryptoService.ComputeSHA256Hash(
                Encoding.UTF8.GetBytes(user.Pin), pinSalt);

            var userEntity = new UserEntity()
            {
                Birth = user.Birth,
                Email = user.Email,
                PasswordHashed = passwordHash,
                PasswordSalt = passwordSalt,
                PinHashed =  pinHash,
                PinSalt = pinSalt
            };

            _context.Users.Add(userEntity);
            _context.SaveChanges();
            return userEntity.Id;
        }
    }
}
