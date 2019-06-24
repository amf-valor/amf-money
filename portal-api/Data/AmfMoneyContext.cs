using AmfValor.AmfMoney.PortalApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AmfValor.AmfMoney.PortalApi.Data
{
    public class AmfMoneyContext :  DbContext
    {
        public DbSet<TradingBook> TradingBooks { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public AmfMoneyContext(){}
        public AmfMoneyContext(DbContextOptions<AmfMoneyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TradingBookConfiguration());
        }
    }
}
