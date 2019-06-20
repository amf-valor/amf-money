using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services.Contract;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly AmfMoneyContext _context;
        public TradeService(AmfMoneyContext context)
        {
            _context = context;
        }
        public void Update(Trade trade)
        {
            _context.Update(trade);
            _context.SaveChanges();
        }
    }
}
