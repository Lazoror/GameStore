using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameModel = GameStore.Domain.Models.SqlModels.GameModels.Game;

namespace GameStore.DAL.EntityConfigurations.Game
{
    public class GameConfiguration : IEntityTypeConfiguration<GameModel>
    {
        public void Configure(EntityTypeBuilder<GameModel> builder)
        {
            builder.ToTable("Game");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasIndex(x => x.Key).IsUnique();

            builder.Property(x => x.Price)
                .HasColumnType("Money");

            builder.Property(x => x.UnitsInStock)
                .HasColumnType("smallint");
        }
    }
}