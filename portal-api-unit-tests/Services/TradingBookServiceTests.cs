using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace PortalApi.UnitTests.Services
{
    public class TradingBookServiceTests
    {
        DbContextOptions<AmfMoneyContext> _options;
        public TradingBookServiceTests()
        {
            _options = new DbContextOptionsBuilder<AmfMoneyContext>()
                .UseInMemoryDatabase(databaseName: "amf_money")
                .Options;
        }

        [Fact]
        public void ShouldCreateNew()
        {
            using (var tradingBookService = new TradingBookService(new AmfMoneyContext(_options)))
            {
                TradingBook expected = GenerateTradingBook();

                TradingBook actual = tradingBookService.Create(expected);

                Assert.True(actual.Id > 0);
                Assert.Equal(0.20, actual.AmountPerCaptal);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.RiskRewardRatio, actual.RiskRewardRatio);
                Assert.NotEqual(default, actual.CreatedAt);
                Assert.Equal(0.01, actual.RiskPerTrade);
                Assert.Equal(expected.TotalCaptal, actual.TotalCaptal);
                Assert.Equal(expected.Trades, actual.Trades);
            }
        }

        [Fact]
        public void ShoulAddToTradingBook()
        {
            Trade expected = new Trade()
            {
                OperationType = 'S'
            };
            using (var tradingBookService = new TradingBookService(new AmfMoneyContext(_options)))
            {
                TradingBook tradingBook = tradingBookService.Create(GenerateTradingBook());

                var addTradeService = new TradingBookService(new AmfMoneyContext(_options));
                Trade actual = addTradeService.AddTo(tradingBook.Id, expected);

                Assert.True(actual.Id > 0);
                Assert.Equal(expected.OperationType, actual.OperationType);
            }
        }

        private TradingBook GenerateTradingBook()
        {
            return new TradingBook()
            {
                AmountPerCaptal = 20,
                Name = "Test",
                RiskRewardRatio = 3,
                RiskPerTrade = 1,
                TotalCaptal = 100000,
                Trades = new List<Trade>()
                {
                    new Trade() { OperationType = 'B'}
                }
            };
        }
    }
}
