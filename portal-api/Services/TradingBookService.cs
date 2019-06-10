using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using System;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class TradingBookService : ITradingBookService
    {
        public TradingBook Create(TradingBook toBeCreated) => new TradingBook()
        {
            Id = 1,
            Name = toBeCreated.Name,
            AmountPerCaptal = toBeCreated.AmountPerCaptal / 100,
            RiskGainRelationship = toBeCreated.RiskGainRelationship,
            CreatedAt = DateTime.Now  
        };
    }
}