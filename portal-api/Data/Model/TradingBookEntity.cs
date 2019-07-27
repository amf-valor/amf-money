using AmfValor.AmfMoney.PortalApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmfValor.AmfMoney.PortalApi.Data.Model
{
    public class TradingBookEntity
    {
        public int Id { get; set; }
        public TradingBookSetting Setting { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public DateTime CreatedAt { get; set; }
        [Column("AccountId")]
        public int AccountEntityId { get; set; }
    }
}

