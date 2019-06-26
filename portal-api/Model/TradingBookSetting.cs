using AmfValor.AmfMoney.PortalApi.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class TradingBookSetting : ValueObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 0.5)]
        public double AmountPerCaptal { get; set; }

        [Required]
        [Range(1, 9999)]
        public int RiskRewardRatio { get; set; }
        public double TotalCaptal { get; set; }

        [Required]
        [Range(0.0001, 1)]
        public double RiskPerTrade { get; set; }

        public TradingBookSetting(){}

        public TradingBookSetting(string name, double amountPerCaptal, int riskRewardRatio, double totalCaptal, double riskPerTrade)
        {
            Name = name;
            AmountPerCaptal = amountPerCaptal;
            RiskRewardRatio = riskRewardRatio;
            TotalCaptal = totalCaptal;
            RiskPerTrade = riskPerTrade;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return AmountPerCaptal;
            yield return RiskRewardRatio;
            yield return TotalCaptal;
            yield return RiskPerTrade;
        }
    }
}
