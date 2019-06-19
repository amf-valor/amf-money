using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using System;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class TradingBookService : ITradingBookService
    {
        private readonly AmfMoneyContext context;
        public TradingBookService(AmfMoneyContext context)
        {
            this.context = context;
        }

        public TradingBook Create(TradingBook toBeCreated)
        {
            TradingBook toBeAdded;

            using (context)
            {
                context.TradingBooks.Add
                (
                    toBeAdded = new TradingBook()
                    {
                        Name = toBeCreated.Name,
                        AmountPerCaptal = toBeCreated.AmountPerCaptal / 100,
                        RiskRewardRatio = toBeCreated.RiskRewardRatio,
                        CreatedAt = DateTime.UtcNow,
                        RiskPerTrade = toBeCreated.RiskPerTrade / 100,
                        TotalCaptal = toBeCreated.TotalCaptal
                    }
                );

                context.SaveChanges();
            }

            return toBeAdded;
        }
    }
}