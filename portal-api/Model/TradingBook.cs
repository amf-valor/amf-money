using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class TradingBook
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 50)]
        public double AmountPerCaptal { get; set; }
        [Required]
        [Range(1, 9999)]
        public int RiskRewardRatio { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [Range(1, 99999999999999999)]
        public double TotalCaptal { get; set; }
        [Required]
        [Range(0.01, 100)]
        public double RiskPerTrade { get; set; }
        public ICollection<Trade> Trades { get; set; }

        public TradingBook()
        {
            Trades = new List<Trade>();
        }
    }
}