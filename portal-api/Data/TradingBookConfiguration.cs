using AmfValor.AmfMoney.PortalApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmfValor.AmfMoney.PortalApi.Data
{
    public class TradingBookConfiguration : IEntityTypeConfiguration<TradingBook>
    {
        public void Configure(EntityTypeBuilder<TradingBook> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreatedAt)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");


            builder.OwnsOne(tb => tb.Setting, setting =>
            {
                setting.Property(t => t.Name)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Name")
                    .IsRequired();

                setting.Property(t => t.AmountPerCaptal)
                    .HasColumnType("decimal(2,2)")
                    .HasColumnName("AmountPerCaptal")
                    .IsRequired();

                setting.Property(t => t.RiskRewardRatio)
                    .HasColumnType("tinyint(1)")
                    .HasColumnName("RiskRewardRatio")
                    .IsRequired();

                setting.Property(t => t.TotalCaptal)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("TotalCaptal")
                    .IsRequired();

                setting.Property(t => t.RiskPerTrade)
                    .HasColumnType("decimal(2,2)")
                    .HasColumnName("RiskPerTrade")
                    .IsRequired();
            });
        }
    }
}
