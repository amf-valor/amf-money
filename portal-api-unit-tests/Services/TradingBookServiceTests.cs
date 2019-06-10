using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using System;
using Xunit;

namespace PortalApi.UnitTests.Services
{
    public class TradingBookServiceTests
    {
        private readonly TradingBookService tradingBookService;

        public TradingBookServiceTests()
        {
            tradingBookService = new TradingBookService();
        }

        [Fact]
        public void ShouldCreateNew()
        {
            TradingBook toBeCreated = new TradingBook()
            {
                AmountPerCaptal = 20,
                Name = "Test",
                RiskGainRelationship = 3
            };

            TradingBook actual = tradingBookService.Create(toBeCreated);

            Assert.True(actual.Id > 0);
            Assert.Equal(0.20, actual.AmountPerCaptal);
            Assert.Equal(toBeCreated.Name, actual.Name);
            Assert.Equal(toBeCreated.RiskGainRelationship, actual.RiskGainRelationship);
            Assert.NotEqual(default, actual.CreatedAt);
        }
    }
}
