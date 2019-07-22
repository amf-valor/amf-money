using AmfValor.AmfMoney.PortalApi.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmfValor.AmfMoney.PortalApi.Data
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.Property(t => t.CreatedAt)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
