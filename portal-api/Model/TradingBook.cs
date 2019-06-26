using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class TradingBook
    {
        public int Id { get; set; }
        public TradingBookSetting Setting { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public DateTime CreatedAt { get; set; }
        public TradingBook()
        {
            Trades = new List<Trade>();
            Setting = new TradingBookSetting();
        }
    }
}