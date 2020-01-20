using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderModel = GameStore.Domain.Models.SqlModels.OrderModels.Order;

namespace GameStore.DAL.EntityConfigurations.Order
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.ToTable("Order");

            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}