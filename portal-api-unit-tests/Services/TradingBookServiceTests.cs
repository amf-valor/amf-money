using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
            AccountEntity account = GenerateAccount();

            using (var context = new AmfMoneyContext(_options))
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }

            using (var tradingBookService = new TradingBookService(new AmfMoneyContext(_options)))
            {

                TradingBookSetting setting = GenerateSetting();

                TradingBookEntity actual = tradingBookService.Create(account.Id, setting);

                Assert.True(actual.Id > 0);
                Assert.Equal(setting, actual.Setting);
                Assert.NotEqual(default, actual.CreatedAt);
                Assert.Null(actual.Trades);
                Assert.True(actual.AccountEntityId > 0);
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
                context.TradingBooks.Add(new TradingBookEntity()
                {
                    Setting = GenerateSetting()
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

        [Theory]
        [ClassData(typeof(TradesTestData))]
        public void ShoulUpdateTrade(List<Trade> trades, int expected)
        {
            using (var context = new AmfMoneyContext(_options))
            {
                context.TradingBooks.Add(new TradingBookEntity()
                {
                    Setting = GenerateSetting()
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

                var actual = tradingBook.Trades.ToList().Count;
                
                Assert.Equal(expected, actual);
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
                context.TradingBooks.Add(new TradingBookEntity()
                {
                    Setting = GenerateSetting()
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
                context.TradingBooks.Add(new TradingBookEntity()
                {
                    Setting = GenerateSetting()
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

        [Fact]
        public void ShouldGetAll()
        {
            AccountEntity account = null;


            using (var context = new AmfMoneyContext(_options))
            {
                account = context.Accounts.Where(a => a.Id == 1).FirstOrDefault();

                if (account == null)
                {
                    account = GenerateAccount();
                    context.Accounts.Add(account);
                    context.SaveChanges();
                }
            }

            using (var context = new AmfMoneyContext(_options))
            {
                TradingBookEntity entity = new TradingBookEntity()
                {
                    Setting = GenerateSetting(),
                    CreatedAt = DateTime.UtcNow,
                    AccountEntityId = account.Id
                };

                context.TradingBooks.Add(entity);
                context.SaveChanges();
            }

            using (var tradingBookService = new TradingBookService(new AmfMoneyContext(_options)))
            {
                ICollection<TradingBookEntity> actual = tradingBookService.GetAll(account.Id);
                Assert.True(actual.Count > 0);
            }
        }

        private TradingBookSetting GenerateSetting() => new TradingBookSetting("test", 20, 4, 100000, 1);
        private AccountEntity GenerateAccount()
        {
            return new AccountEntity()
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                Birth = DateTime.Now,
                Email = "teste@teste.com",
                HashedPassword = new byte[] {1,2},
                PasswordSalt = new byte[] { 1, 2 },
                HashedPin = new byte[] { 1, 2 },
                PinSalt = new byte[] { 1, 2 }
            };
        }

        private class TradesTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new List<Trade>()
                    {
                        new Trade()
                        {
                            Id = 0,
                            OperationType = 'S',
                            Asset = "PETR4",
                            Price = 6.80M,
                            Quantity = 1,
                            StopGain = 7.50M,
                            StopLoss = 5.50M
                        }
                    },
                    1
                };
                yield return new object[]
                {
                    new List<Trade>()
                    {
                        new Trade()
                        {
                            Id = 0,
                            OperationType = 'S',
                            Asset = "PETR4",
                            Price = 6.80M,
                            Quantity = 1,
                            StopGain = 7.50M,
                            StopLoss = 5.50M
                        },
                        new Trade()
                        {
                            Id = 0,
                            OperationType = 'S',
                            Asset = "PETR4",
                            Price = 6.80M,
                            Quantity = 1,
                            StopGain = 7.50M,
                            StopLoss = 5.50M
                        }
                    },
                    2
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
