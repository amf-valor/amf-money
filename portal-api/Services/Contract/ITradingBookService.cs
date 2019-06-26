using AmfValor.AmfMoney.PortalApi.Model;
using System.Collections.Generic;

namespace AmfValor.AmfMoney.PortalApi.Services.Contract
{
    public interface ITradingBookService
    {
        TradingBook Create(TradingBookSetting theSetting);
        void Update(int tradingBookId, ICollection<Trade> trades);
        ICollection<TradingBook> GetAll();
        void Update(int id, TradingBookSetting setting);
    }
}