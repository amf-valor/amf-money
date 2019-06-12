using System;
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
    }
}