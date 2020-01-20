using GameStore.Domain.Models.SqlModels.GameModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.Game
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.ToTable("GameGenre");

            builder.HasKey(x => new { x.GameId, x.GenreId });
        }
    }
}