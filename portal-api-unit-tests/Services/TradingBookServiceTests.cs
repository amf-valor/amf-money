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
            TradingBook toBeCreated = new TradingBook()
            {
                AmountPerCaptal = 20,
                Name = "Test",
                RiskRewardRatio = 3
            };

            TradingBook actual = tradingBookService.Create(toBeCreated);

            Assert.True(actual.Id > 0);
            Assert.Equal(0.20, actual.AmountPerCaptal);
            Assert.Equal(toBeCreated.Name, actual.Name);
            Assert.Equal(toBeCreated.RiskRewardRatio, actual.RiskRewardRatio);
            Assert.NotEqual(default, actual.CreatedAt);
        }
    }
}
