using AmfValor.AmfMoney.PortalApi.Model;

namespace AmfValor.AmfMoney.PortalApi.Services.Contract
{
    public interface ITradingBookService
    {
        TradingBook Create(TradingBook toBeCreated);
        Trade AddTo(int tradingBookId, Trade newTrade);
    }
}