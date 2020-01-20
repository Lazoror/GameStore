using GameStore.Domain.Models.LanguageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.LanguageEntities
{
    public class GameTranslationConfiguration : IEntityTypeConfiguration<GameTranslation>
    {
        public void Configure(EntityTypeBuilder<GameTranslation> builder)
        {
            builder.ToTable("GameTranslation");

            builder.HasOne(x => x.Game).WithMany(x => x.Languages)
                .HasForeignKey(x => x.EntityId);
        }
    }
}