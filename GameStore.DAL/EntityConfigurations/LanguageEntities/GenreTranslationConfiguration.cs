using GameStore.Domain.Models.LanguageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.LanguageEntities
{
    public class GenreTranslationConfiguration : IEntityTypeConfiguration<GenreTranslation>
    {
        public void Configure(EntityTypeBuilder<GenreTranslation> builder)
        {
            builder.ToTable("GenreTranslation");

            builder.HasOne(x => x.Genre).WithMany(x => x.Languages)
                .HasForeignKey(x => x.EntityId);
        }
    }
}