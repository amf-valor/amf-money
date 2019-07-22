using AmfValor.AmfMoney.PortalApi.Model;

namespace AmfValor.AmfMoney.PortalApi.Services.Contract
{
    public interface IAccountService
    {
        bool CheckIfExists(string email);
        int SignUp(Account account);
    }
}
