using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                TradingBookSetting setting = Generate();

                TradingBook actual = tradingBookService.Create(setting);

                Assert.True(actual.Id > 0);
                Assert.Equal(setting, actual.Setting);
                Assert.NotEqual(default, actual.CreatedAt);
                Assert.True(actual.Trades.Count == 0);
            }
        }

        [Fact]
        public void ShoulAddNewTrade()
        {
            var trades = new List<Trade>()
            {
                new Trade()
                {
                    OperationType = 'S',
                    Asset = "BOVA11",
                    Price = 50.80M,
                    Quantity = 10,
                    StopGain = 60.50M,
                    StopLoss = 40.50M
                }
            };

            using (var context = new AmfMoneyContext(_options))
            {
                context.TradingBooks.Add(new TradingBook()
                {
                    Setting = Generate()
                });
                context.SaveChanges();
            }

            using (var service = new TradingBookService(new AmfMoneyContext(_options)))
            {
                service.Update(1, trades);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                var tradingBook = context.TradingBooks
                .Where(tb => tb.Id == 1)
                .Include(tb => tb.Trades)
                .FirstOrDefault();

                var actual  = tradingBook.Trades.ToList()[0];
                var expected = trades[0];
                Assert.True(actual.Id > 0);
                Assert.Equal(expected.OperationType, actual.OperationType);
                Assert.Equal(expected.Price, actual.Price);
                Assert.Equal(expected.Quantity, actual.Quantity);
                Assert.Equal(expected.StopGain, actual.StopGain);
                Assert.Equal(expected.StopLoss, actual.StopLoss);
            }
        }

        [Fact]
        public void ShoulUpdateTrade()
        {
            var trades = new List<Trade>()
            {
                new Trade()
                {
                    Id = 1,
                    OperationType = 'S',
                    Asset = "PETR4",
                    Price = 6.80M,
                    Quantity = 1,
                    StopGain = 7.50M,
                    StopLoss = 5.50M
                }
            };

            using (var context = new AmfMoneyContext(_options))
            {
                context.TradingBooks.Add(new TradingBook()
                {
                    Setting = Generate()
                });
                context.SaveChanges();
            }

            using (var service = new TradingBookService(new AmfMoneyContext(_options)))
            {
                service.Update(1, trades);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                var tradingBook = context.TradingBooks
                .Where(tb => tb.Id == 1)
                .Include(tb => tb.Trades)
                .FirstOrDefault();

                var actual = tradingBook.Trades.ToList()[0];
                var expected = trades[0];
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.OperationType, actual.OperationType);
                Assert.Equal(expected.Price, actual.Price);
                Assert.Equal(expected.Quantity, actual.Quantity);
                Assert.Equal(expected.StopGain, actual.StopGain);
                Assert.Equal(expected.StopLoss, actual.StopLoss);
            }
        }

        [Fact]
        public void ShoulRemoveTrade()
        {
            var trades = new List<Trade>()
            {
                
            };

            using (var context = new AmfMoneyContext(_options))
            {
                context.TradingBooks.Add(new TradingBook()
                {
                    Setting = Generate()
                });
                context.SaveChanges();
            }

            using (var service = new TradingBookService(new AmfMoneyContext(_options)))
            {
                service.Update(1, trades);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                var tradingBook = context.TradingBooks
                .Where(tb => tb.Id == 1)
                .Include(tb => tb.Trades)
                .FirstOrDefault();

                var actual = tradingBook.Trades.ToList();
                Assert.True(actual.Count == 0);
            }
        }

        [Fact]
        public void ShouldUpdateSetting()
        {
            TradingBookSetting expected = new TradingBookSetting("expected", 0.10, 10, 9000, 0.06);

            using (var context = new AmfMoneyContext(_options))
            {
                context.TradingBooks.Add(new TradingBook()
                {
                    Setting = Generate()
                });
                context.SaveChanges();
            }

            using (var service = new TradingBookService(new AmfMoneyContext(_options)))
            {
                service.Update(1, expected);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                var actual = context.TradingBooks
                .Where(tb => tb.Id == 1)
                .Include(tb => tb.Trades)
                .FirstOrDefault().Setting;

                Assert.Equal(expected, actual);
            }
        }

        private TradingBookSetting Generate() => new TradingBookSetting("test", 20, 4, 100000, 1);
    }
}
