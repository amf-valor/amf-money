using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class TradingBookService : Service, ITradingBookService
    {
        public TradingBookService(AmfMoneyContext context): base(context) { }
        public TradingBookEntity Create(int accountId, TradingBookSetting setting)
        {
            AccountEntity account = _context.Accounts.Find(accountId);

            if (account == null)
            {
                throw new AccountNotFoundException($"Account with id: {accountId} does not exists");
            }

            TradingBookEntity toBeAdded = new TradingBookEntity()
            {
                Setting = setting,
                CreatedAt = DateTime.UtcNow,
                AccountEntityId = account.Id
            };

            _context.TradingBooks.Add(toBeAdded);
            _context.SaveChanges();
            return toBeAdded;
        }
        public void Update(int tradingBookId, ICollection<Trade> trades)
        {
            TradingBookEntity tradingBook = _context.TradingBooks
                .Where(tb => tb.Id == tradingBookId)
                .Include(tb => tb.Trades)
                .FirstOrDefault();
                
            if (tradingBook == null)
                throw new TradingBookNotFoundException($"trading book with id {tradingBookId} was not found!");

            var newTrades = new List<Trade>();

            foreach (var trade in trades) 
            {
                var existingTrade = tradingBook.Trades
                .Where(t => t.Id == trade.Id)
                .SingleOrDefault();

                if (existingTrade == null)
                {
                    newTrades.Add(trade);
                }
                else
                {
                    _context.Entry(existingTrade).CurrentValues.SetValues(trade);
                }
            }

            if (newTrades.Count > 0)
            {
                foreach (var newTrade in newTrades)
                {
                    tradingBook.Trades.Add(newTrade);
                }
            }

            foreach (var trade in tradingBook.Trades)
            {
                if (!trades.Any(t => t.Id == trade.Id))
                    _context.Trades.Remove(trade);
            }

            _context.SaveChanges();
        }
        public ICollection<TradingBookEntity> GetAll(int accountId)
        {
            return _context.TradingBooks
                    .Where(tb => tb.AccountEntityId == accountId)
                    .Include(t => t.Trades)
                    .ToList();
        }
        public void Update(int tradingBookId, TradingBookSetting setting)
        {
            TradingBookEntity tradingBook = _context.TradingBooks
                .Where(tb => tb.Id == tradingBookId)
                .FirstOrDefault();

            if (tradingBook == null)
                throw new TradingBookNotFoundException($"trading book with id {tradingBookId} was not found!");

            tradingBook.Setting = setting;
            _context.SaveChanges();
        }
    }
}