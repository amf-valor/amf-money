using System;

namespace AmfValor.AmfMoney.PortalApi.Model
{
    public class TradingBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AmountPerCaptal { get; set; }
        public int RiskGainRelationship { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}