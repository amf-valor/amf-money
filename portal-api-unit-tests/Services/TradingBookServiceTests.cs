using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PortalApi.UnitTests.Services
{
    public class TradingBookServiceTests
    {
        private readonly TradingBookService tradingBookService;

        public TradingBookServiceTests()
        {
            var options = new DbContextOptionsBuilder<AmfMoneyContext>()
                .UseInMemoryDatabase(databaseName: "amf_money")
                .Options;

            tradingBookService = new TradingBookService(new AmfMoneyContext(options));
        }

        [Fact]
        public void ShouldCreateNew()
        {
            TradingBook expected = new TradingBook()
            {
                AmountPerCaptal = 20,
                Name = "Test",
                RiskRewardRatio = 3,
                RiskPerTrade = 1,
                TotalCaptal = 100000
            };

            TradingBook actual = tradingBookService.Create(expected);

            Assert.True(actual.Id > 0);
            Assert.Equal(0.20, actual.AmountPerCaptal);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.RiskRewardRatio, actual.RiskRewardRatio);
            Assert.NotEqual(default, actual.CreatedAt);
            Assert.Equal(0.01, actual.RiskPerTrade);
            Assert.Equal(expected.TotalCaptal, actual.TotalCaptal);
        }
    }
}
