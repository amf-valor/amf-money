using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class TradingBookService : ITradingBookService, IDisposable
    {
        private readonly AmfMoneyContext _context;
        public TradingBookService(AmfMoneyContext context)
        {
            _context = context;
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

        public void Update(int tradingBookId, ICollection<Trade> trades)
        {
            TradingBook tradingBook = _context.TradingBooks
                .Where(tb => tb.Id == tradingBookId)
                .Include(tb => tb.Trades)
                .FirstOrDefault();
                
            if (tradingBook == null)
                throw new TradingBookNotFoundException($"trading book with id {tradingBookId} was not found!");

            foreach (var trade in trades) 
            {
                var existingTrade = tradingBook.Trades
                .Where(t => t.Id == trade.Id)
                .SingleOrDefault();

                if (existingTrade == null)
                {
                    tradingBook.Trades.Add(trade);
                }
                else
                {
                    _context.Entry(existingTrade).CurrentValues.SetValues(trade);
                }
            }

            foreach (var trade in tradingBook.Trades)
            {
                if (!trades.Any(t => t.Id == trade.Id))
                    _context.Trades.Remove(trade);
            }

            _context.SaveChanges();
        }
        
    }
}