using AmfValor.AmfMoney.PortalApi.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmfValor.AmfMoney.PortalApi.Data
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(t => t.CreatedAt)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
