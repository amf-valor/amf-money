using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PortalApi.UnitTests.Services
{
    public class TradeServiceTests
    {
        DbContextOptions<AmfMoneyContext> _options;

        public TradeServiceTests()
        {
            _options = new DbContextOptionsBuilder<AmfMoneyContext>()
                .UseInMemoryDatabase(databaseName: "amf_money")
                .Options;
        }

        [Fact]
        public void ShouldUpdate()
        {
            using (var context = new AmfMoneyContext(_options))
            {
                context.Add(new Trade()
                {
                    OperationType = 'B'
                });

                context.SaveChanges();
            }

            using (var context = new AmfMoneyContext(_options))
            {
                var service = new TradeService(context);
                var trade = context.Find<Trade>(1);
                trade.OperationType = 'S';
                service.Update(trade);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                var trade = context.Find<Trade>(1);
                Assert.Equal('S', trade.OperationType);
            }

        }


    }
}
