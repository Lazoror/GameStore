using GameStore.Domain.Models.SqlModels.GameModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Game
{
    public class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
    {
        public void Configure(EntityTypeBuilder<GamePlatform> builder)
        {
            builder.ToTable("GamePlatform");

            builder.HasKey(x => new { x.GameId, x.PlatformTypeId });
        }
    }
}