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

            builder.Property(t => t.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(t => t.AmountPerCaptal)
                .HasColumnType("decimal(2,2)")
                .IsRequired();

            builder.Property(t => t.RiskGainRelationship)
                .HasColumnType("tinyint(1)")
                .IsRequired();

        }
    }
}
