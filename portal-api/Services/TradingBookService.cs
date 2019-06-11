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
                        RiskGainRelationship = toBeCreated.RiskGainRelationship,
                        CreatedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }

            return toBeAdded;
        }
    }
}