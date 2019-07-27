using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using System.Collections.Generic;

namespace AmfValor.AmfMoney.PortalApi.Services.Contract
{
    public interface ITradingBookService
    {
        TradingBookEntity Create(int accountId, TradingBookSetting theSetting);
        void Update(int tradingBookId, ICollection<Trade> trades);
        ICollection<TradingBookEntity> GetAll(int accountId);
        void Update(int id, TradingBookSetting setting);
    }
}