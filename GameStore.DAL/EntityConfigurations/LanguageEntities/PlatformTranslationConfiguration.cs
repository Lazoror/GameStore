using GameStore.Domain.Models.LanguageModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.EntityConfigurations.LanguageEntities
{
    public class PlatformTranslationConfiguration : IEntityTypeConfiguration<PlatformTranslation>
    {
        public void Configure(EntityTypeBuilder<PlatformTranslation> builder)
        {
            builder.ToTable("PlatformTranslation");

            builder.HasOne(x => x.Platform).WithMany(x => x.Languages)
                .HasForeignKey(x => x.EntityId);
        }
    }
}