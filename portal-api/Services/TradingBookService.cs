using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using System;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class TradingBookService : ITradingBookService, IDisposable
    {
        private readonly AmfMoneyContext _context;
        public TradingBookService(AmfMoneyContext context)
        {
            _context = context;
        }
        public Trade AddTo(int tradingBookId, Trade newTrade)
        {
            TradingBook tradingBook = _context.Find<TradingBook>(tradingBookId);

            if (tradingBook == null)
                throw new TradingBookNotFoundException($"trading book with id {tradingBookId} was not found!");

            tradingBook.Trades.Add(newTrade);
            _context.SaveChanges();
            return newTrade;
        }
        public TradingBook Create(TradingBook toBeCreated)
        {
            TradingBook toBeAdded;

            _context.TradingBooks.Add
            (
                toBeAdded = new TradingBook()
                {
                    Name = toBeCreated.Name,
                    AmountPerCaptal = toBeCreated.AmountPerCaptal / 100,
                    RiskRewardRatio = toBeCreated.RiskRewardRatio,
                    CreatedAt = DateTime.UtcNow,
                    RiskPerTrade = toBeCreated.RiskPerTrade / 100,
                    TotalCaptal = toBeCreated.TotalCaptal,
                    Trades = toBeCreated.Trades
                }
            );

            _context.SaveChanges();
            return toBeAdded;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}