using GameStore.Domain.Models.SqlModels.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Order
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.GameKey)
                .HasColumnType("nvarchar(40)");

            builder.Property(x => x.Price)
                .HasColumnType("Money");

            builder.Property(x => x.Quantity)
                .HasColumnType("smallint");

            builder.Property(x => x.Discount)
                .HasColumnType("Real");
        }
    }
}