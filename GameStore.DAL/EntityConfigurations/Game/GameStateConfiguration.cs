using GameStore.Domain.Models.SqlModels.GameModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Game
{
    public class GameStateConfiguration : IEntityTypeConfiguration<GameState>
    {
        public void Configure(EntityTypeBuilder<GameState> builder)
        {
            builder.ToTable("GameState");
        }
    }
}